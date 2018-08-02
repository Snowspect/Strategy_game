using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_windows;
using Strategy_game.Func;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Strategy_game.GUI
{
    /// <summary>
    /// Interaction logic for FieldWindow.xaml
    /// </summary>
    public partial class FieldWindow : Window, IFieldWindow_Impl<int, string>
    {
        //TODO Could change tuple to contain a coordinate and a participants, then change the coordinate to also have a field that tells whether it is occupied or not
        //so if the field is occupied, check what participant it is, and if on opposite team then battle, otherwize if on same field, deny move action, otherwize move to field.
        #region localVariables
        NameScope ScopeName = new NameScope();
        private MainWindow mw;
        private Window w;
        private Boolean exitApp, backtrack;
        Game_Logic_Impl gli;
        #endregion

        #region constructors
        //Not used either
        public FieldWindow()
        {
            InitializeComponent();
        }

        public FieldWindow(Window w, Game_Logic_Impl gimpl)
        {
            gli = gimpl;
            this.w = w;

            if (w is PreBattleFieldWindow)
            {
                backtrack = true;
            }

            InitializeComponent();

            CreatePreField();

            //Adds prefieldbattle team to list and adds them to their respective fields on the battleField
            foreach (var item in gimpl.GetField())
            {
                ListOfParticipants.Items.Add(item.Item1.NameGS);
                Image ima = new Image();
                //gets image from participant to move.
                string image = gli.GetImage(item.Item1.NameGS);

                //Gets fieldCoords from participant
                string fieldName = gli.GetParticipantFieldCoord(item.Item1.NameGS);
                //find designated spot on field
                ima = (Image)FieldGrid.FindName(fieldName);
                ima.Stretch = Stretch.Fill;
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
        }

        //this constructor will be removed in the future as it is used as a quick test access from mainwindow
        public FieldWindow(MainWindow mw, Window w, Game_Logic_Impl gimpl)
        {
            gli = gimpl;
            exitApp = true; //used for closing app 
            this.mw = mw;
            this.w = w;
            Closed += new EventHandler(App_exit); //subscribing to closed event 

            InitializeComponent();

            /* Test Section START */


            foreach (var item in gimpl.GetField())
            {
                ListOfParticipants.Items.Add(item.Item1.NameGS);
            }
            /* Test Section END */
        }
        #endregion

        #region event triggered methods
        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) { w.Close(); } /*closes mainWindow*/ }

        private void XCoord_TextChanged(object sender, TextChangedEventArgs e) { if (xCoord.Text.Length > 0) HintXCoord.Visibility = Visibility.Hidden; else HintXCoord.Visibility = Visibility.Visible; }

        private void YCoord_TextChanged(object sender, TextChangedEventArgs e) { if (yCoord.Text.Length > 0) HintYCoord.Visibility = Visibility.Hidden; else HintYCoord.Visibility = Visibility.Visible; }

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        { /*do not close mw.*/ exitApp = false; /*loads mainWindow*/ if (w is MainWindow) { this.w.Show(); this.Close(); } /*loads any other window */ else { w = new Window(); w.Show(); this.Close(); } }

        //Loads mainwindow if not coming from preField otherwize backtracks. ((refer to prefield closed event handler))
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e)
        {
            if (backtrack == true)
            {
                this.Close();

                //do nothing
            }
            else { mw.Show(); exitApp = false; this.Close(); }
        }
        #endregion

        #region buttons
        //Activates player movement
        private void SubmitMove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (xCoord.Text == "" || yCoord.Text == "")
            { Console.WriteLine("The boxes was empty"); }
            else
            {
                MoveToSpot();

                xCoord.Clear();
                yCoord.Clear();
                PlayingDisplayBox.Background = Brushes.Green;
            }
        }
        #endregion

        #region Methods
        //Moves participant in fieldDTO List
        //sets and clears images as well
        public void MoveToSpot()
        {
            int x = int.Parse(xCoord.Text);
            int y = int.Parse(yCoord.Text);
            string participantToMove = ListOfParticipants.SelectedItem.ToString(); //retrieves name 

            ClearsImage(x, y, participantToMove);

            //moves participant in storage and on field list 
            //Updates participantDTO in storage 
            gli.MoveParticipant(x, y, participantToMove);

            SetsImage(x, y, participantToMove);
        }
        public void ClearsImage(int xCoord, int yCoord, string participant_name)
        {
            string fieldCoord = gli.GetParticipantFieldCoord(participant_name); //retrieves current Coords 

            Image ima = new Image();
            ima = (Image)FieldGrid.FindName(fieldCoord); //finds image with x:Name that matches coords 
            ima.ClearValue(Image.SourceProperty); //clears the image 
                                                  
            //do a if check to see what team they are on and then color the field specifically after that.
            Participant_Impl t = new Participant_Impl();
            string teamColor = t.GetParticipant(participant_name).TeamColorGS;
            Console.WriteLine(teamColor);
            if (teamColor == "purple")
            {
                string image = "purpleField.png";
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
            else if(teamColor == "blue")
            {
                string image = "blueField.png";
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
        }
        public void SetsImage(int xCoord, int yCoord, string participant_name)
        {
            Image ima = new Image();
            //gets image from participant to move.
            string image = gli.GetImage(participant_name);

            //finds the image field based on the coords
            string fieldName = "x" + xCoord + "y" + yCoord;
            ima = (Image)FieldGrid.FindName(fieldName);
            ima.Stretch = Stretch.Fill;
            ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
        }
        public void CreatePreField()
        {
            UserControl u;
            int j = 6;
            int i = 6;
            int h = 0;
            FieldGrid.Rows = 6;
            FieldGrid.Columns = 6;
            h = j;
            //Fills out a uniform grid with pictures of the same slime "currently", needs to fill out with a ground tile
            for (int k = 1; k < j + 1; k++) //k is 1, increased to 6
            {
                for (int g = 1; g < i + 1; g++) // g is 1, increased to 3 
                {
                    u = new UserControl();
                    Border b = new Border();
                    b.BorderBrush = new SolidColorBrush(Colors.Black);
                    b.BorderThickness = new Thickness(1);
                    string xName = "x" + g + "y" + h; //g goes 1,2,3...  //h goes 6,5,4 repeatedly. (should go, 6,6,6 and then 5,5,5)

                    Image img = new Image();
                    img.Stretch = Stretch.Fill;
                    string image = "UnoccupiedField.png";
                    NameScope.SetNameScope(this, ScopeName); //only way to access the x:Name variable
                    ScopeName.RegisterName(xName, img); //Only way to access the x:Name variable
                    img.Source = new BitmapImage(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
                    b.Child = img;

                    u.Content = b;
                    FieldGrid.Children.Add(u);
                }
                h--;
            }
        }
        #endregion
    }
}