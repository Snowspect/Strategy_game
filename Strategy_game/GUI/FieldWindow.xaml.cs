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
        private MainWindow mw; 
        private Window w; 
        private Boolean exitApp, backtrack; 
        Game_Logic_Impl gli; 

        public FieldWindow()
        {
            InitializeComponent();
        }

        public FieldWindow(Window w, Game_Logic_Impl gimpl)
        {
            gli = gimpl;
            //exitApp = true; //used for closing app 
            this.w = w;
            
            if (w is PreBattleFieldWindow)
            {
                backtrack = true;
            }

            InitializeComponent();

            /* Test Section START */

            // Inserts image into site. (not sure how the path works)
            x1y6.Stretch = Stretch.Fill;
            x1y6.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\SlimeBlack.png"));

            foreach (var item in gimpl.GetField())
            {
                ListOfParticipants.Items.Add(item.Item1.NameGS);

            }
            /* Test Section END */
        }

        public FieldWindow(MainWindow mw, Window w, Game_Logic_Impl gimpl)
        {
            gli = gimpl; 
            exitApp = true; //used for closing app 
            this.mw = mw; 
            this.w = w; 
            Closed += new EventHandler(App_exit); //subscribing to closed event 
            
            InitializeComponent();

            /* Test Section START */

            // Inserts image into site. (not sure how the path works)
            x1y6.Stretch = Stretch.Fill;
            x1y6.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\SlimeBlack.png"));   

            foreach (var item in gimpl.GetField())
            {
                ListOfParticipants.Items.Add(item.Item1.NameGS);
                Image ima = new Image();
                //gets image from participant to move.
                string image = gli.GetImage(item.Item1.NameGS);

                //finds the image field based on the coords
                string fieldName = "x" + xCoord + "y" + yCoord;
                ima = (Image)FieldGrid.FindName(fieldName);
                ima.Stretch = Stretch.Fill;
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
            /* Test Section END */
        }

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

        //Activates player movement
        private void SubmitMove_Button_Click(object sender, RoutedEventArgs e)
        {
            if(xCoord.Text == "" || yCoord.Text == "")
            { Console.WriteLine("The boxes was empty"); }
            else
            {
                MoveToSpot();

                xCoord.Clear();
                yCoord.Clear();
                PlayingDisplayBox.Background = Brushes.Green;
            }
        }

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
    } 
} 