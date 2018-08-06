using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_windows;
using Strategy_game.Func;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy_game.GUI
{
    /// <summary>
    /// Interaction logic for PreBattleFieldWindow.xaml
    /// </summary>
    public partial class PreBattleFieldWindow : Window, IPreBattleFieldWindow_Impl<string, ArenaFieldPoint_DTO, Participant_DTO>
    {
        #region localVariables
        MainWindow mw;
        Window w;
        private Boolean exitApp;
        Participant_Impl pImpl = new Participant_Impl();
        Arena_Impl ArenaImpl;
        Team_Impl tImpl;
        ArenaWindow fw;
        NameScope ScopeName = new NameScope();
        int skinCounter = 0;
        FieldPoint_Impl fPImpl;
        #endregion

        #region constructors
        public PreBattleFieldWindow()
        {
            InitializeComponent();
        }
        public PreBattleFieldWindow(MainWindow mw, Window w, Participant_Impl pImpl)
        {
            this.ArenaImpl = new Arena_Impl();
            fPImpl = new FieldPoint_Impl();
            this.pImpl = pImpl;
            this.w = w;
            this.mw = mw;
            tImpl = new Team_Impl();
            Closed += new EventHandler(App_exit); //subscribing to closed event
            exitApp = true; //used for closing app
            InitializeComponent();
            CreatePreArena();
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
            string image = tImpl.GetAllyTeamImage(TeamListBox.SelectedValue.ToString());
            TeamImage.Stretch = Stretch.Fill;
            TeamImage.Source = (new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image + ".png")));

        }

        private void XCoord_TextChanged(object sender, TextChangedEventArgs e)
        { if (txtXCoord.Text.Equals("")) { HintXCoord.Visibility = Visibility.Visible; } else HintXCoord.Visibility = Visibility.Hidden; }
        private void YCoord_TextChanged(object sender, TextChangedEventArgs e)
        { if (txtYCoord.Text.Equals("")) { HintYCoord.Visibility = Visibility.Visible; } else HintYCoord.Visibility = Visibility.Hidden; }

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
        public void CreatePreArena()
        {
            UserControl u;
            int j = 6;
            int i = 3;

            PreArena.Rows = 6;
            PreArena.Columns = 3;

            //Fills out a uniform grid with pictures of the same slime "currently", needs to fill out with a ground tile 
            for (int h = j; h > 0; h--) //k is 1, increased to 6 
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
                    PreArena.Children.Add(u);
                }
            }

            ArenaImpl.CreateFullArena();
        }
        public void ShowTeamList()
        {
            foreach (var item in tImpl.GetAllyTeamList())
            {
                TeamListBox.Items.Add(item.Key);
            }
        }
        public void ShowTeamMemberLists(string team)
        {
            foreach (var item in pImpl.GetCurrentList())
            {
                if (item.TeamGS.Equals(team)) MemberListBox.Items.Add(item.NameGS);
            }
        }

        public void ClearsImage(Participant_DTO pDTO)
        {
            if (pDTO != null) //checks if parsed participant is actually existing
            {
                if (pDTO.PointGS.XPoint != 0 && pDTO.PointGS.YPoint != 0) //enters if its default coords isn't {0,0} 
                {
                    string fieldCoord = pDTO.PointGS.ToString();
                    Image ima = (Image)PreArena.FindName(fieldCoord); //finds image with x:Name that matches coords 
                    if(ima != null)
                    {
                        ima.ClearValue(Image.SourceProperty); //clears the image 
                        string image = "UnoccupiedField.png";
                        ima.Source = new BitmapImage(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
                    }
               }
            }
        }
        public void SetsImage(Participant_DTO pDTO)
        {
            Image ima = new Image();
            //gets image from participant to move.
            string image = pDTO.ImageGS;

            //finds the image field based on the coords
            string arenaFieldName = pDTO.PointGS.ToString();

            ima = (Image)ScopeName.FindName(arenaFieldName);
            ima.Stretch = Stretch.Fill;
            ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
        }

        //Generates list of coords for enemy team
        public List<ArenaFieldPoint_DTO> GenerateCoordsList(string arena)
        {
            List<ArenaFieldPoint_DTO> tmpList = new List<ArenaFieldPoint_DTO>();
            Random rand;
            bool run = true, AllowedAdd = true;
            int counter = 0;

            if (!arena.Equals("preArena"))
            {
                while (run)
                {
                    ArenaFieldPoint_DTO AFP = new ArenaFieldPoint_DTO();
                    rand = new Random();
                    AFP.XPoint = rand.Next(4,7);
                    AFP.YPoint = rand.Next(1, 6);
                    if (tmpList.Count != 0)
                    {
                        foreach (var item in tmpList)
                        {
                            if (item.XPoint == AFP.XPoint && item.YPoint == AFP.YPoint) //checks to see if the designated coord is already taken.
                            {
                                AllowedAdd = false;
                            }
                        }
                        if (AllowedAdd == true) { tmpList.Add(AFP); counter++; }
                        if (counter == 6)
                        {
                            run = false;
                        }
                    }
                    AllowedAdd = true;
                    if (counter == 0) { counter++; tmpList.Add(AFP); }
                }
            } //returns a set of arena coords for actualArena
            else
            {
                while (run)
                {
                    ArenaFieldPoint_DTO AFP = new ArenaFieldPoint_DTO();
                    rand = new Random();
                    AFP.XPoint = rand.Next(1, 4);
                    AFP.YPoint = rand.Next(1, 6);
                    if (tmpList.Count != 0)
                    {
                        foreach (var item in tmpList)
                        {
                            if (item.XPoint == AFP.XPoint && item.YPoint == AFP.YPoint) //checks to see if the designated coord is already taken.
                            {
                                AllowedAdd = false;
                            }
                        }
                        if (AllowedAdd == true) { tmpList.Add(AFP); counter++; }
                        if (counter == 6)
                        {
                            run = false;
                        }
                    }
                    AllowedAdd = true;
                    if (counter == 0) { counter++; tmpList.Add(AFP); }
                }
            } //returns a set of arena coords for preArena
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
            string participantToMove = MemberListBox.SelectedItem.ToString(); //retrieves name
            Participant_DTO pDTO = pImpl.GetParticipant(participantToMove);
            pDTO.TeamColorGS = "purple";
            int x = int.Parse(txtXCoord.Text);
            int y = int.Parse(txtYCoord.Text);
            ArenaFieldPoint_DTO AFP_DTO = fPImpl.GetArenaField(x, y);

            /** MOVING **/
            bool run = fPImpl.CheckField(AFP_DTO, pDTO); //checks the field we are trying to go to

            if (run)
            {
                ClearsImage(pDTO); //removes image

                //updates the point we are leaving.
                fPImpl.UpdateLeavingArenaFieldPoint(pDTO, "preArena");  //Updating the fieldstatus since we are leaving to another field.

                pImpl.MoveParticipant(pDTO, fPImpl.GetArenaField(x, y));

                fPImpl.UpdateMovingToArenaFieldStatus(AFP_DTO, "preArena");

                SetsImage(pDTO);
            }
            /** MOVING ENDS **/
        }

        private void ClearField_Button(object sender, RoutedEventArgs e)
        {
            foreach (ArenaFieldPoint_DTO AFP_DTO in ArenaImpl.GetField())
            {
                ClearsImage(AFP_DTO.PDTO);
                if (AFP_DTO.PDTO != null)  AFP_DTO.PDTO.PointGS = null;
                AFP_DTO.PDTO = null;
            }
            //ArenaImpl.EmptyField();
            skinCounter = 0;
            //TODO Lock team select while one member from a team is placed on the field.
        }
        //Triggered when clicking "Start fight"
        //configures team to be purple team and adds a skin to each of them
        private void StartBattle_Button(object sender, RoutedEventArgs e)
        {
            StartBattle();
        }

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        { /*do not close mw.*/ exitApp = false; /*loads mainWindow*/ if (w is MainWindow) { this.w.Show(); this.Close(); } /*loads any other window */ else { w = new Window(); w.Show(); this.Close(); } }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); exitApp = false; this.Close(); }

        #endregion

        private void Preset_Click(object sender, RoutedEventArgs e)
        {
            List<ArenaFieldPoint_DTO> RandomArenaFields = GenerateCoordsList("preArena");
            if (RandomArenaFields.Count != 0) //to make sure we don't go to next window.
            {
                string allyTeam = tImpl.GetAllyTeamName();
                int coordCounter = 0; //used to give each player a set of coords
                skinCounter = 0;
                foreach (Participant_DTO pDTO in tImpl.GetAllyTeam(allyTeam))
                {
                    pDTO.TeamColorGS = "purple";
                    //Gets field from arena based on x and y coords from random field list.
                    pDTO.PointGS = fPImpl.GetArenaField(RandomArenaFields[coordCounter].XPoint, RandomArenaFields[coordCounter].YPoint);
                    pDTO.ImageGS = Storage.PlayerSkins[skinCounter];
                    ArenaImpl.AddParticipantToField(pDTO);
                    fPImpl.UpdateMovingToArenaFieldStatus(pDTO.PointGS, "preArena"); //moves to field and occupies that field
                    skinCounter++;
                    coordCounter++;
                }
            }
            StartBattle();
        }

        public void StartBattle()
        {
            List<ArenaFieldPoint_DTO> tmpFPList = new List<ArenaFieldPoint_DTO>();
            tmpFPList = GenerateCoordsList("actualArena");

            if (tmpFPList.Count != 0) //to make sure we don't go to next window.
            {
                string enemyTeam = tImpl.GetEnemyTeamName();
                int coordCounter = 0; //used to give each player a set of coords
                skinCounter = 0;
                foreach (var item in tImpl.GetEnemyTeam(enemyTeam))
                {
                    item.PointGS = tmpFPList[coordCounter];
                    item.ImageGS = Storage.PlayerSkins[skinCounter];
                    ArenaImpl.AddParticipantToField(item);
                    skinCounter++;
                    coordCounter++;
                }
                //test for team name and then color respectively, also here, find random enemy team.
                List<ArenaFieldPoint_DTO> tmp = ArenaImpl.GetField(); //DEBUGGING
                foreach (ArenaFieldPoint_DTO AFP_DTO in ArenaImpl.GetField())
                {
                    if (AFP_DTO.PDTO != null)
                    {
                        if (AFP_DTO.PDTO.TeamGS.Equals(TeamListBox.SelectedValue.ToString()))
                        {
                            AFP_DTO.PDTO.TeamColorGS = "purple";
                        }
                        else
                        {
                            AFP_DTO.PDTO.TeamColorGS = "Blue";
                            AFP_DTO.PDTO.PointGS.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.enemyOccupied;
                        }
                    }
                }
                fw = new ArenaWindow(this, ArenaImpl);
                fw.Closed += new EventHandler(Reference);
                fw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                fw.Show();
                this.Hide();
            }
        }

        public List<ArenaFieldPoint_DTO> GenerateCoordsList()
        {
            throw new NotImplementedException();
        }
    }
}