using Strategy_game.Data;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_windows;
using Strategy_game.Func;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy_game.GUI
{
    /// <summary>
    /// Interaction logic for FieldWindow.xaml
    /// </summary>
    public partial class ArenaWindow : Window, IFieldWindow_Impl<int, string>
    {
        #region localVariables
        NameScope ScopeName = new NameScope();
        private MainWindow mw;
        private Window w;
        private Boolean exitApp, backtrack;
        Arena_Impl arenaImpl;
        Participant_Impl pImpl;
        FieldPoint_Impl fPImpl;
        #endregion

        #region constructors
        //Not used either
        public ArenaWindow() => InitializeComponent();

        public ArenaWindow(Window w, Arena_Impl arenaImpl)
        {
            pImpl = new Participant_Impl();
            fPImpl = new FieldPoint_Impl();
            this.arenaImpl = arenaImpl;
            this.w = w;

            //is triggered when coming from prefield window
            if (w is PreBattleFieldWindow)
            {
                backtrack = true;
            }

            InitializeComponent();

            //ALREADY CREATED ARENA IN PREARENA
            //Creates the playground (currently of 6,6)
            CreateArena();

            //Adds prefieldbattle team to list and adds them to their respective fields on the battleField
            InsertParticipantsToField();

        }
        #endregion

        /*
         * App_exit
         * xCoord_TextChanged
         * yCoord_TextChanged
         * ToPreviousWindow
         * ToMenu
         */
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

        /*
         * SubmitMove_Button
         */
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

        /*
         * MoveToSpot
         * ClearsImage
         * SetsImage
         * CreatePreField
         * InsertParticipantsToField
         */
        #region Methods
        //Moves participant in fieldDTO List
        //sets and clears images as well
        public void MoveToSpot()
        {
            int x = int.Parse(xCoord.Text);
            int y = int.Parse(yCoord.Text);

            //moves participant in storage and on field list 
            //Updates participantDTO in storage
            string participantToMove = ListOfParticipants.SelectedItem.ToString(); //retrieves name 
            Participant_DTO pDTO = pImpl.GetParticipant(participantToMove);

            /** MOVING **/
            bool run = fPImpl.CheckField(x, y); //checks the field we are trying to go to

            if (run)
            {
                ClearsImage(pDTO); //removes image

                //updates the point we are leaving.
                fPImpl.UpdateLeavingArenaFieldPoint(pDTO, "actualArena");  //Updating the fieldstatus since we are leaving to another field.
                Console.WriteLine(Arena_DTO.field);
                pImpl.MoveParticipant(pDTO, fPImpl.GetArenaField(x, y));
                Console.WriteLine(Arena_DTO.field);
                fPImpl.UpdateMovingToArenaFieldStatus(x, y);

                SetsImage(pDTO);
            }
            /** MOVING ENDS **/
        }
        public void ClearsImage(Participant_DTO pDTO)
        {
            Image ima = new Image();
            ima = (Image)FieldGrid.FindName(pDTO.PointGS.ToString()); //finds image with x:Name that matches coords 
            ima.ClearValue(Image.SourceProperty); //clears the image 
                                                  
            //do a if check to see what team they are on and then color the field specifically after that.
            string teamColor = pDTO.TeamColorGS;
            if (teamColor == "purple")
            {
                string image = "purpleField.png";
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));

            }
            else if(teamColor == "Blue")
            {
                string image = "blueField.png";
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
        }
        public void SetsImage(Participant_DTO pDTO)
        {
            Image ima = new Image();
            //gets image from participant to move.
            string image = pDTO.ImageGS;

            //finds the image field based on the coords
            string fieldName = pDTO.PointGS.ToString();
            ima = (Image)FieldGrid.FindName(fieldName);
            ima.Stretch = Stretch.Fill;
            ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
        }
        public void CreateArena()
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
        public void InsertParticipantsToField()
        {
            foreach (ArenaFieldPoint_DTO AFP_DTO in arenaImpl.GetField())
            {
                if(AFP_DTO.PDTO != null)
                {
                    //this should be the line when the enemy can move itself
                    //if(AFP_DTO.PDTO.TeamGS.Equals("purple")) ListOfParticipants.Items.Add(AFP_DTO.PDTO.NameGS);
                    ListOfParticipants.Items.Add(AFP_DTO.PDTO.NameGS);

                    //gets image from participant to move.
                    string image = AFP_DTO.PDTO.ImageGS;

                    //Gets fieldCoords from AFP_DTO that has a participant
                    string fieldName = AFP_DTO.ToString();
                    //find designated spot on field
                    Image ima = (Image)FieldGrid.FindName(fieldName);
                    ima.Stretch = Stretch.Fill;
                    ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
                }
            }
        }



        public void ClearsImage(int xCoord, int yCoord, string participant_name)
        {
            throw new NotImplementedException();
        }
        public void SetsImage(int xCoord, int yCoord, string participant_name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}