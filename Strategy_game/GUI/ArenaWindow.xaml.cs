using Strategy_game.Data;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_windows;
using Strategy_game.Func;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Strategy_game.GUI
{

    public partial class ArenaWindow : Window, IFieldWindow_Impl<Fighter_DTO, ArenaFieldPoint_DTO>
    {
        #region localVariables
        private NameScope ScopeName = new NameScope();
        private Window w;
        private Boolean exitApp, backtrack;

        private string hordeTxt = "Horde is playing, please press the 'Activate Horde' button";
        private string allianceTxt = "Alliance is playing, please enter coords and submit your move";
        private Arena_Impl arenaImpl;
        private Fighter_Impl pImpl;
        private Boolean gameNotFinished = true;

        #endregion

        #region constructors
        //Not used either
        public ArenaWindow() => InitializeComponent();

        public ArenaWindow(Window w, Arena_Impl arenaImpl, Fighter_Impl pImpl)
        {
            this.pImpl = pImpl; 

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

            ListOfParticipants.SelectedIndex = 0;
            Fighter_DTO firstFighter = pImpl.GetParticipant(ListOfParticipants.SelectedValue.ToString()); //is allowed due to not otherwize being able to access it.
            if (firstFighter.TeamColorGS.Equals("blue")) //only triggered if the first fighter is Horde
            {
                xCoord.IsHitTestVisible = false;
                yCoord.IsHitTestVisible = false;
                PlayingDisplayBox.Text = hordeTxt;
                CheckNextParticipant();
            }
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

        //Loads mainwindow if not coming from preField otherwize backtracks. ((refer to prefield closed event handler))
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e)
        {
            if (backtrack == true)
            {
                this.Close();

                //do nothing
            }
        }
        #endregion

        /*
         * SubmitMove_Button
         */
        #region buttons
        //Activates player movement
        private void SubmitMove_Button_Click(object sender, RoutedEventArgs e)
        {
            ActivateAllianceTurn();
            UpdateList();
            CheckNextParticipant(); //should stop if the next one is alliance, otherwize execute horde code
            //CheckNextParticipant();
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
        public void InitiateMovement(ArenaFieldPoint_DTO AFP_DTO)
        {
            //moves participant in storage and on field list 
            //Updates participantDTO in storage 
            string participantToMove = ListOfParticipants.SelectedItem.ToString(); //retrieves name 

            Fighter_DTO pDTO = pImpl.GetParticipant(participantToMove);

            bool movementOkay = pImpl.CheckMovement(pDTO, AFP_DTO); //first checks if movement is okay

            if (movementOkay == true)
            {
                bool checkField = arenaImpl.CheckField(AFP_DTO, pDTO); //checks the field we are trying to go to

                if (checkField == true)
                {
                    ActivateMovement(pDTO, AFP_DTO);
                }
            }
        }

        //is called by both alliance and horde
        public void ActivateMovement(Fighter_DTO pDTO, ArenaFieldPoint_DTO AFP_DTO)
        {
            ClearsImage(pDTO); //removes image

            //updates the point we are leaving.
            arenaImpl.UpdateLeavingArenaFieldPoint(pDTO, "actualArena");  //Updating the fieldstatus since we are leaving to another field.

            pImpl.MoveParticipant(pDTO, AFP_DTO);

            arenaImpl.UpdateMovingToArenaFieldStatus(AFP_DTO, "actualArena");

            SetsImage(pDTO);
        }

        public void ClearsImage(Fighter_DTO pDTO)
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
            else if (teamColor == "blue")
            {
                string image = "blueField.png";
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
        }
        public void SetsImage(Fighter_DTO pDTO)
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

        //only called initially
        public void InsertParticipantsToField()
        {
            arenaImpl.SetActivePlayers(Shuffle(GetActivePlayers()));
            foreach (Fighter_DTO pDTO in Arena_DTO.ActiveFighters)
            {
                ListOfParticipants.Items.Add(pDTO.NameGS);
                //gets image from participant to insert visually
                string image = pDTO.ImageGS;

                //Gets fieldCoords from AFP_DTO so we can find the appropriate visual field to insert our image
                string fieldName = pDTO.PointGS.ToString();
                //find designated spot on field
                Image ima = (Image)FieldGrid.FindName(fieldName);
                ima.Stretch = Stretch.Fill;
                ima.Source = new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image));
            }
        }

        //is called on first run if enemy is horde
        private void ActivateEnemy_Click(object sender, RoutedEventArgs e)
        {
            Fighter_DTO currentFighter = pImpl.GetParticipant(ListOfParticipants.SelectedValue.ToString());
            ActivateHordeTurn(currentFighter);
        }

        /// <summary>
        /// Checks if the game is a new game
        /// Adds all fighters to the list
        /// //checks if the top fighter is horde and if, starts fight, else start alliance
        /// </summary>
        /// <returns></returns>
        public void CheckNextParticipant()
        {
            ListOfParticipants.SelectedIndex = 0; //Selects first index
            Fighter_DTO currentFighter = pImpl.GetParticipant(ListOfParticipants.SelectedValue.ToString());
            if (currentFighter.TeamColorGS.Equals("blue"))
            {
                ActivateHordeTurn(currentFighter);
                UpdateList();
                CheckIfGameFinished();
                if(gameNotFinished == true)
                {
                    CheckNextParticipant(); //calls itself ((should be avoided), could end up on infinite stack
                }
            }
            else
            {
                xCoord.IsHitTestVisible = true;
                yCoord.IsHitTestVisible = true; 
            }
        }

        //shuffes the first player to the back
        //and removes any player that no longer exists within the activePlayers list
        public void UpdateList()
        {
            string PlayedFighter = ListOfParticipants.Items.GetItemAt(0).ToString();
            ListOfParticipants.Items.RemoveAt(0);
            ListOfParticipants.Items.Add(PlayedFighter);
            for(int i = 0; i < ListOfParticipants.Items.Count; i++)
            {
                //foreach (Participant_DTO pDTO in Arena_DTO.ActiveFighters) //checks all outside with names in list and removes outside if not in list. weird impl. all wrong
                //{
                //    if(!ListOfParticipants.Items.Contains(pDTO.NameGS))
                //    {
                //        ListOfParticipants.Items.Remove(pDTO.NameGS);
                //    }
                //}
                ListOfParticipants.SelectedIndex = i;
                bool allowedToRemove = false;
                int confirmCounter = 0;
                foreach (Fighter_DTO fighter in Arena_DTO.ActiveFighters)
                {

                    string test = ListOfParticipants.SelectedItem.ToString();
                    if (!fighter.NameGS.Equals(test)) //goes true if the selected index doesn't match our current fighter check... doesn't work. it can't match any of them
                    {
                        confirmCounter += 1;
                    }
                    if(confirmCounter == Arena_DTO.ActiveFighters.Count)
                    {
                        allowedToRemove = true;
                    }
                }
                if(allowedToRemove == true) //if the name doesn't exist within active players
                {
                    ListOfParticipants.Items.Remove(ListOfParticipants.SelectedItem.ToString());
                }
            }
        }

        /// <summary>
        /// Gets list of active players (only used at the beginning of the fight)
        /// </summary>
        /// <returns></returns>
        public List<Fighter_DTO> GetActivePlayers()
        {
            List<Fighter_DTO> ActivePlayers = new List<Fighter_DTO>();
            foreach (ArenaFieldPoint_DTO AFP_DTO in arenaImpl.GetField())
            {
                if (AFP_DTO.PDTO != null)
                {
                    ActivePlayers.Add(AFP_DTO.PDTO);
                }
            }
            return ActivePlayers;
        }


        //Takes entered coords and moves alliance player
        public void ActivateAllianceTurn()
        {
            if (xCoord.Text == "" || yCoord.Text == "")
            {
                MessageBoxResult res = MessageBox.Show("please enter some coords");
            }
            else
            {
                int x = int.Parse(xCoord.Text);
                int y = int.Parse(yCoord.Text);
                ArenaFieldPoint_DTO AFP_DTO = new ArenaFieldPoint_DTO();
                AFP_DTO = arenaImpl.GetArenaField(x, y);
                InitiateMovement(AFP_DTO); //this AFP_DTO is the field we wish to move to

                xCoord.Clear();
                yCoord.Clear();
            }
        }


        /// <summary>
        /// Gets the alliance fighters within range and attacks a random one
        /// if there is none to attack, move to a random spot within your range
        /// cannot move to a field where another horde fighter is.
        /// </summary>
        public void ActivateHordeTurn(Fighter_DTO currentFighter)
        {
            //check felter omkring
            //vælge en modstander tilfældigt af dem som er tilgængelige hvis der er nogle.
            //flyt til det felt
            List<ArenaFieldPoint_DTO> FieldsWithAllianceToAttack = pImpl.CheckSurroundingFields(currentFighter);
            Random r = new Random();
            ArenaFieldPoint_DTO AFP_DTO;

            if (FieldsWithAllianceToAttack.Count != 0)
            {
                int chosenenemy = r.Next(0, FieldsWithAllianceToAttack.Count);
                AFP_DTO = FieldsWithAllianceToAttack[chosenenemy];
                InitiateMovement(AFP_DTO);
            }
            else
            {
                //Gets fields that no horde member is on
                List<ArenaFieldPoint_DTO> EmptyNearbyArenaFields = new List<ArenaFieldPoint_DTO>();
                foreach (ArenaFieldPoint_DTO AFP_DTO_local in pImpl.GetMovementRange(currentFighter))
                {
                    if (AFP_DTO_local.FieldPointStatusGS != FieldStatus_DTO.FieldStatus.HordeOccupied)
                    {
                        EmptyNearbyArenaFields.Add(AFP_DTO_local);
                    }
                }
                if (EmptyNearbyArenaFields.Count != 0)
                {
                    AFP_DTO = EmptyNearbyArenaFields[r.Next(0, EmptyNearbyArenaFields.Count)];
                    InitiateMovement(AFP_DTO);
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show("all nearby fields are occupied");
                }
            }
        }

        //Shuffles a list and returns it
        public List<Fighter_DTO> Shuffle(List<Fighter_DTO> turnBasedMovementList)
        {
            Random rng = new Random();
            int n = turnBasedMovementList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Fighter_DTO value = turnBasedMovementList[k];
                turnBasedMovementList[k] = turnBasedMovementList[n];
                turnBasedMovementList[n] = value;
            }
            return turnBasedMovementList;
        }

        public void CheckIfGameFinished()
        {
            if(arenaImpl.CheckTeamLists() == true)
            {
                gameNotFinished = false;
            }
        }
        #endregion

    }
}