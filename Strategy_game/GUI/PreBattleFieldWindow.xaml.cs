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
    public partial class PreBattleFieldWindow : Window, IPreBattleFieldWindow_Impl<string, ArenaFieldPoint_DTO, Fighter_DTO>
    {
        #region localVariables
        private string AllianceTeamColor = "purple";
        private string HordeTeamColor = "blue";
        private MainWindow mw;
        private Boolean exitApp;
        private Fighter_Impl pImpl;
        private Arena_Impl ArenaImpl;
        private Team_Impl tImpl;
        NameScope ScopeName = new NameScope();
        int skinCounter = 0;
        #endregion

        #region constructors
        public PreBattleFieldWindow()
        {
            InitializeComponent();
        }
        public PreBattleFieldWindow(MainWindow mw, Fighter_Impl pImpl, Arena_Impl arenaImpl, Team_Impl tImpl)
        {
            this.ArenaImpl = arenaImpl;

            this.pImpl = pImpl;
            this.mw = mw;
            this.tImpl = tImpl;
            Closed += new EventHandler(App_exit); //subscribing to closed event
            exitApp = true; //used for closing app
            InitializeComponent();
            CreatePreArena();
            ShowTeamList();
        }
        #endregion

        #region event triggered Methods Excluding buttons

        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) {} /*closes mainWindow*/ }


        // Event related to ArenaWindow Closed event.
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

        #region methods
        /// <summary>
        /// Fills out Horde team with coords and skins
        /// Distributes all members in fields to either alliance or horde team ("purple or blue team respectively")
        /// Starts up the Arena Window
        /// </summary>
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
                    item.ImageGS = pImpl.GetHordeskin(skinCounter);// Storage.HordeSkins[skinCounter];
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
                            AFP_DTO.PDTO.TeamColorGS = AllianceTeamColor;
                            Arena_DTO.AllianceTeam.Add(AFP_DTO.PDTO);
                        }
                        else
                        {
                            AFP_DTO.PDTO.TeamColorGS = HordeTeamColor;
                            AFP_DTO.PDTO.PointGS.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.HordeOccupied;
                            Arena_DTO.HordeTeam.Add(AFP_DTO.PDTO);
                        }
                    }
                }
                ArenaWindow fw = new ArenaWindow(this, ArenaImpl, pImpl);
                fw.Closed += new EventHandler(Reference);
                fw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                fw.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// visuallly fills pre-arena grid with UnoccupiedField images
        /// </summary
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

        /// <summary>
        /// shows lists of teams
        /// </summary>
        public void ShowTeamList()
        {
            foreach (var item in tImpl.GetAllyTeamList())
            {
                TeamListBox.Items.Add(item.Key);
            }
        }

        /// <summary>
        /// when a team is selected, it will display the members in it
        /// </summary>
        /// <param name="team"></param>
        public void ShowTeamMemberLists(string team)
        {
            foreach (var item in pImpl.GetCurrentList())
            {
                if (item.TeamGS.Equals(team)) MemberListBox.Items.Add(item.NameGS);
            }
        }

        /// <summary>
        /// replaces the image with an unoccupiedField image on the field that a fighter is leaving.
        /// ((Not identical to ClearsImage in ArenaWindow))
        /// </summary>
        /// <param name="pDTO"> The fighter that is moving </param>
        public void ClearsImage(Fighter_DTO pDTO)
        {
            if (pDTO != null) //checks if parsed participant is actually existing
            {
                if (pDTO.PointGS.XPoint != 0 && pDTO.PointGS.YPoint != 0) //enters if its default coords isn't {0,0} 
                {
                    string fieldCoord = pDTO.PointGS.ToString();
                    Image ima = (Image)PreArena.FindName(fieldCoord); //finds image with x:Name that matches coords 
                    if (ima != null)
                    {
                        ima.ClearValue(Image.SourceProperty); //clears the image 
                        string image = "UnoccupiedField.png";
                        ima.Source = new BitmapImage(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
                    }
                }
            }
        }

        /// <summary>
        /// Sets the image of the field the fighter is moving to to it's own image
        /// </summary>
        /// <param name="pDTO"> The fighter that is moving </param>
        public void SetsImage(Fighter_DTO pDTO)
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

        /// <summary>
        /// Generates a list of random coordinates within the allowed range.
        /// </summary>
        /// <param name="arena"> a simply string to check whether we are in pre-arena or the actual arena</param>
        /// <returns> a list of random coordinates within an allowed range </returns>
        public List<ArenaFieldPoint_DTO> GenerateCoordsList(string arena)
        {
            List<ArenaFieldPoint_DTO> tmpList = new List<ArenaFieldPoint_DTO>();
            Random rand;
            bool run = true, AllowedAdd = true;
            int counter = 0;
            
            while (run)
            {
                ArenaFieldPoint_DTO AFP = new ArenaFieldPoint_DTO();
                if (!arena.Equals("preArena")) //triggered if for Horde half
                {
                    rand = new Random();
                    AFP.XPoint = rand.Next(4, 7);
                    AFP.YPoint = rand.Next(1, 7);
                }
                else //triggered if for Alliance half
                {
                    rand = new Random();
                    AFP.XPoint = rand.Next(1, 4);
                    AFP.YPoint = rand.Next(1, 6);
                }

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
            return tmpList;
        }
        #endregion

        #region buttons

        /// <summary>
        /// event triggered when player submitting a fighter to the pre-arena.
        /// "a combination of other methods primarily"
        /// ((Not used if pressing the "preset" button))
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitMove_Button(object sender, RoutedEventArgs e)
        {
            string participantToMove = MemberListBox.SelectedItem.ToString();
            Fighter_DTO pDTO = pImpl.GetParticipant(participantToMove);                     
            pDTO.TeamColorGS = AllianceTeamColor;                                                        
            int x = int.Parse(txtXCoord.Text);
            int y = int.Parse(txtYCoord.Text);
            ArenaFieldPoint_DTO AFP_DTO = ArenaImpl.GetArenaField(x, y);

            /** MOVING **/
            bool run = ArenaImpl.CheckField(AFP_DTO, pDTO); //checks the field we are trying to go to

            if (run)
            {
                ClearsImage(pDTO); //removes image

                //updates the point we are leaving.
                ArenaImpl.UpdateLeavingArenaFieldPoint(pDTO, "preArena");  //Updating the fieldstatus since we are leaving to another field.

                pImpl.MoveParticipant(pDTO, ArenaImpl.GetArenaField(x, y));

                ArenaImpl.UpdateMovingToArenaFieldStatus(AFP_DTO, "preArena");

                SetsImage(pDTO);
            }
            /** MOVING ENDS **/
        }

        /// <summary>
        /// replaces all images with an unoccupiedField image and sets all arena_field fighter references to null
        /// resets skincounter as we need it when submitting them manually
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearField_Button(object sender, RoutedEventArgs e)
        {
            foreach (ArenaFieldPoint_DTO AFP_DTO in ArenaImpl.GetField())
            {
                ClearsImage(AFP_DTO.PDTO);
                if (AFP_DTO.PDTO != null) AFP_DTO.PDTO.PointGS = null;
                AFP_DTO.PDTO = null;
            }
            //ArenaImpl.EmptyField();
            skinCounter = 0;
            //TODO Lock team select while one member from a team is placed on the field.
        }

        /// <summary>
        /// Starts the battle with the team that has been manually put in by the player 
        /// ((not using preset button))
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartBattle_Button(object sender, RoutedEventArgs e)
        {
            StartBattle();
        }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); exitApp = false; this.Close(); }

        /// <summary>
        /// Auto fills the alliance team and gives each fighter a skin
        /// triggers the StartBattle() method that also triggers the horde team initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Preset_Click(object sender, RoutedEventArgs e)
        {
            List<ArenaFieldPoint_DTO> RandomArenaFields = GenerateCoordsList("preArena");
            string allyTeam = tImpl.GetAllyTeamName();
            int coordCounter = 0; //used to give each player a set of coords
            skinCounter = 0; //used to iterate through skins

            // gives skins and coords to each member on the Alliance team
            foreach (Fighter_DTO pDTO in tImpl.GetAllyTeam(allyTeam))
            {
                pDTO.TeamColorGS = AllianceTeamColor;
                //Gets field from arena based on x and y coords from random field list.
                pDTO.PointGS = ArenaImpl.GetArenaField(RandomArenaFields[coordCounter].XPoint, RandomArenaFields[coordCounter].YPoint);
                pDTO.ImageGS = pImpl.GetAllianceSkin(skinCounter);
                ArenaImpl.AddParticipantToField(pDTO);
                ArenaImpl.UpdateMovingToArenaFieldStatus(pDTO.PointGS, "preArena"); //moves to field and occupies that field
                skinCounter++;
                coordCounter++;
            }
            StartBattle();
        }
        #endregion
    }
}