using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Data.DTO;
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
    /// Interaction logic for PreBattleFieldWindow.xaml
    /// </summary>
    public partial class PreBattleFieldWindow : Window
    {
        #region localVariables
        MainWindow mw;
        Window w;
        private Boolean exitApp;
        Participant_Impl pImpl = new Participant_Impl();
        Game_Logic_Impl gli;
        FieldWindow fw;
        NameScope ScopeName = new NameScope();
        int skinCounter = 0;
        #endregion

        #region constructors
        public PreBattleFieldWindow()
        {
            InitializeComponent();
        }
        public PreBattleFieldWindow(MainWindow mw, Window w, Game_Logic_Impl gli, Participant_Impl pImpl)
        {
            this.gli = gli;
            this.pImpl = pImpl;
            this.w = w;
            this.mw = mw;
     
            Closed += new EventHandler(App_exit); //subscribing to closed event
            exitApp = true; //used for closing app
            InitializeComponent();
            CreatePreField();
            ShowTeamList();
        }
        #endregion

        /**
         * app_exit, Reference
         * SelectionChanged
         * TextChanged
         */
        #region event triggered Methods
        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) { w.Close(); } /*closes mainWindow*/ }

        // Event related to fieldWindow Closed event.
        //makes sure mainwindow doesn't gets closed when backtracking.
        void Reference(object sender, EventArgs e) { mw.Show(); exitApp = false; this.Close(); }

        private void TeamListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string team = TeamListBox.SelectedItem.ToString();
            ShowTeamMemberLists(team);
            string image = pImpl.GetTeamImage(TeamListBox.SelectedValue.ToString());
            TeamImage.Stretch = Stretch.Fill;
            TeamImage.Source = (new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image + ".png")));

        }

        private void XCoord_TextChanged(object sender, TextChangedEventArgs e)
        { if (xCoord.Text.Equals("")) { HintXCoord.Visibility = Visibility.Visible; } else HintXCoord.Visibility = Visibility.Hidden; }
        private void YCoord_TextChanged(object sender, TextChangedEventArgs e)
        { if (yCoord.Text.Equals("")) { HintYCoord.Visibility = Visibility.Visible; } else HintYCoord.Visibility = Visibility.Hidden; }

        #endregion

        /**
         * CreatePreField
         * ShowTeamList
         * ShowTeamMemberList
         * MoveToSpot
         * ClearsImage
         * SetsImage
         * GenerateCoordsList
         */
        #region methods
        //Fills created grid
        public void CreatePreField()
        {
            UserControl u;
            int j = 6;
            int i = 3;
            int h = 0;
            PreFieldBattle.Rows = 6;
            PreFieldBattle.Columns = 3;
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
                    PreFieldBattle.Children.Add(u);
                }
                h--;
            }
        }
        private void ShowTeamList()
        {
            foreach (var item in pImpl.GetTeamList())
            {
                TeamListBox.Items.Add(item.Key);
            } 
        }
        private void ShowTeamMemberLists(string team)
        {
            foreach (var item in pImpl.GetCurrentList())
            {
                if (item.TeamGS.Equals(team)) MemberListBox.Items.Add(item.NameGS);
            }
        }

        //moves selected player to spot.
        public void MoveToSpot()
        {
            string participantToMove = MemberListBox.SelectedItem.ToString(); //retrieves name

            Participant_DTO t = new Participant_DTO();
            t = pImpl.GetParticipant(participantToMove);

            t.ImageGS = Storage.PlayerSkins[skinCounter];
            skinCounter++;
            
            #region NotaddedToFieldTwice
            //Makes sure participant isn't added to field twice.
            bool addOrNot = true;
            foreach (var item in gli.GetField())
            {
                if (item.Item1.NameGS.Equals(participantToMove))
                {
                    addOrNot = false;
                }
            }
            if (addOrNot == true)
            {

                gli.AddParticipantToField(t);
            }
            #endregion

            int x = int.Parse(xCoord.Text);
            int y = int.Parse(yCoord.Text);

            ClearsImage(x, y, participantToMove);

            //moves participant in storage and on field list
            //Updates participantDTO in storage
            gli.MoveParticipant(x, y, participantToMove);

            SetsImage(x, y, participantToMove);
        }
        public void ClearsImage(int xCoord, int yCoord, string participant_name)
        {
            if (!gli.GetParticipantFieldCoord(participant_name).Equals("x0y0")) //not sure what i ment to do here
            {
                string fieldCoord = gli.GetParticipantFieldCoord(participant_name); //retrieves current Coords

                Image ima = new Image();
                ima = (Image)PreFieldBattle.FindName(fieldCoord); //finds image with x:Name that matches coords 
                ima.ClearValue(Image.SourceProperty); //clears the image 
                string image = "UnoccupiedField.png";
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

            Console.WriteLine(PreFieldBattle);

            ima = (Image)ScopeName.FindName(fieldName);
            ima.Stretch = Stretch.Fill;
            ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
        }

        public List<FieldPoint_DTO> GenerateCoordsList()
        {
            List<FieldPoint_DTO> tmpList = new List<FieldPoint_DTO>();
            Random rand1 = new Random();
            Random rand2 = new Random();
            bool run = true, AllowedAdd = true;
            int counter = 0;
            
            while (run)
            {
                FieldPoint_DTO fp = new FieldPoint_DTO();
                fp.XPoint = rand1.Next(4, 6);
                fp.YPoint = rand2.Next(0, 6);
                if (tmpList.Count != 0)
                {
                    foreach (var item in tmpList)
                    {
                        if (item.XPoint == fp.XPoint && item.YPoint == fp.YPoint) //checks to see if the designated coord is already taken.
                        {
                            AllowedAdd = false;
                        }
                    }
                    if (AllowedAdd == true) { tmpList.Add(fp); counter++; }
                    if (counter == 6)
                    {
                        run = false;
                    }
                }
                AllowedAdd = true;
                if (counter == 0) { counter++; tmpList.Add(fp); }
            }
            return tmpList;
        }
        #endregion

        /**
         * SubmitMove
         * ClearField
         * StartBattle
         * ToPreviousWindow
         * ToMenuWindow
         */
        #region buttons
        private void SubmitMove_Button(object sender, RoutedEventArgs e)
        {
            MoveToSpot();
        }

        private void ClearField_Button(object sender, RoutedEventArgs e)
        {
            //TODO Lock team select while one member from a team is placed on the field.
        }
        //Triggered when clicking "Start fight"
        //configures team to be purple team and adds a skin to each of them
        private void StartBattle_Button(object sender, RoutedEventArgs e)
        {
            List<FieldPoint_DTO> tmpFPList = new List<FieldPoint_DTO>();
            tmpFPList = GenerateCoordsList();

            if (tmpFPList.Count != 0) //to make sure we don't go to next window.
            {
                string enemyTeam = pImpl.GetEnemyTeamName();
                int coordCounter = 0;

                foreach (var item in pImpl.GetEnemyTeam(enemyTeam))
                {
                    item.PointGS = tmpFPList[coordCounter];
                    gli.AddParticipantToField(item);
                    coordCounter++;
                }
                //test for team name and then color respectively, also here, find random enemy team.
                foreach (var item in gli.GetField())
                {
                    if (item.Item1.TeamGS.Equals(TeamListBox.SelectedValue.ToString()))
                    {
                        item.Item1.TeamColorGS = "purple";
                    }
                    else
                    {
                        item.Item1.TeamColorGS = "blue";
                    }
                }
                fw = new FieldWindow(this, gli);
                fw.Closed += new EventHandler(Reference);
                fw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                fw.Show();
                this.Hide();
            }
        } 

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        { /*do not close mw.*/ exitApp = false; /*loads mainWindow*/ if (w is MainWindow) { this.w.Show(); this.Close(); } /*loads any other window */ else { w = new Window(); w.Show(); this.Close(); } }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); exitApp = false; this.Close(); }

        #endregion
    }
}