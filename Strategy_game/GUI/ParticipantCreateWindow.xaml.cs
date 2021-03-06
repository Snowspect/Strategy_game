﻿using Microsoft.Win32;
using Strategy_game.Data;
using Strategy_game.Data.Interface_windows;
using Strategy_game.Exceptions;
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
    /// Interaction logic for ParticipantCreateWindow.xaml
    /// </summary>
    public partial class ParticipantCreateWindow : Window, ICreateParticipantWindow_Impl<int, Fighter_DTO>
    {
        #region localVariables
        private MainWindow mw;
        private Window w;
        private Boolean exitApp;
        private Fighter_Impl pImpl;
        private Team_Impl tImpl;
        private string TeamImageName;
        #endregion

        #region constructors
        public ParticipantCreateWindow() => InitializeComponent();
        
        //Constructor to take two kinds of windows
        public ParticipantCreateWindow(MainWindow mw, Window w, Fighter_Impl pImpl, Team_Impl tImpl)
        {
            this.pImpl = pImpl;
            this.tImpl = tImpl;
            exitApp = true; //used for closing app
            this.w = w;
            this.mw = mw;
            InitializeComponent();
            Closed += new EventHandler(App_exit); //subscribing to closed event

            /*Test section (new team box) START */
            CreateTeamBox.Visibility = Visibility.Hidden;
            // Inserts image into site. (not sure how the path works)
            
            CoverTeamCanvasImage.Stretch = Stretch.Fill;
            CoverTeamCanvasImage.Source = new BitmapImage(new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\SlimeBlack.png"));
            CoverTeamCanvasImage.Visibility = Visibility.Visible;
            CoverTeamCanvasImage.Margin = new Thickness(556,358,0,0);

            /*Test section (new team box) END */

            //List<Participant_DTO> participantList = pImpl.GetCurrentList();
            foreach (var item in pImpl.GetCurrentList())
            {
                StrongAgainstFirstChoice.Items.Add(item.NameGS);
                StrongAgainstSecondChoice.Items.Add(item.NameGS);
                WeakAgainstFirstChoice.Items.Add(item.NameGS);
                WeakAgainstSecondChoice.Items.Add(item.NameGS);
                ImmuneAgainstFirstChoice.Items.Add(item.NameGS);
                ImmuneAgainstSecondChoice.Items.Add(item.NameGS);
            }
            foreach (var item in tImpl.GetAllyTeamList())
            {
                TeamNameChoice.Items.Add(item.Key);
            }
        }
        #endregion

        /*
         * App_exit
         * TeamNameChoice_SelectionChanged (gets the image related to the selected team and displays it)
         * A bunch of textChanged events that show/hides hints in input boxes
         */
        #region event triggered methods
        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) { w.Close(); } /*closes mainWindow*/ }

        private void TeamNameChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TeamNameChoice.SelectedIndex != -1)
            {
                string image = tImpl.GetAllyTeamImage(TeamNameChoice.SelectedValue.ToString());
                displayTeamImage.Stretch = Stretch.Fill;
                displayTeamImage.Source = (new BitmapImage(new Uri(System.IO.Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\" + image + ".png")));
            }
        }

        //triggered when Text Changes within name box (all hide or show the hint box)
        #region textChanged
        private void TeamNameBox_TextChanged(object sender, TextChangedEventArgs e) { if (TeamNameTextBox.Text == "") TeamNameHint.Visibility = Visibility.Visible; else TeamNameHint.Visibility = Visibility.Hidden; }
        private void NameTextChanged(object sender, TextChangedEventArgs e) { if (NameTextBox.Text.Length >0) HintName.Visibility = Visibility.Hidden; else HintName.Visibility = Visibility.Visible; }
        private void HealthTextBox_TextChanged(object sender, TextChangedEventArgs e) { if(HealthTextBox.Text.Length > 0) HintHealth.Visibility = Visibility.Hidden; else HintHealth.Visibility = Visibility.Visible; }
        private void OffenceTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (OffenceTextBox.Text.Length > 0) HintOffence.Visibility = Visibility.Hidden; else HintOffence.Visibility = Visibility.Visible; }
        private void DefenceTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (DefenceTextBox.Text.Length > 0) HintDefence.Visibility = Visibility.Hidden; else HintDefence.Visibility = Visibility.Visible; }
        private void HMoveTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (MoveTextBox.Text.Length > 0) HintMove.Visibility = Visibility.Hidden; else HintMove.Visibility = Visibility.Visible; }
        #endregion

        #endregion

        /*
         * SubmitParticipant (submits participant to created participants through several checks and procedures)
         * NewTeamWindow (shows a small window.. normally hidden behind a small picture.. that let's you create a new ally team
         * SubmitTeam..creates the new ally team
         * InsertImage (opens dialog window to choose image for team)
         * ToPreviousWindow
         * ToMenuWindow
         */
        #region buttons


        /// <summary>
        /// Takes all inputs and creates a participant and adds it to a team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitParticipant_Click(object sender, RoutedEventArgs e)
        {
            List<int> parsedTP = new List<int>();
            List<string> tp = new List<string>(); //tp = tryParse
            tp.Add(HealthTextBox.Text);
            tp.Add(OffenceTextBox.Text);
            tp.Add(DefenceTextBox.Text);
            tp.Add(MoveTextBox.Text);

            parsedTP = pImpl.ParseInts(tp);

            if (NameTextBox.Text == "" || HealthTextBox.Text == "" || OffenceTextBox.Text == "" || DefenceTextBox.Text == ""
            || MoveTextBox.Text == "" || StrongAgainstFirstChoice.SelectedIndex == -1 || StrongAgainstSecondChoice.SelectedIndex == -1
            || WeakAgainstFirstChoice.SelectedIndex == -1 || WeakAgainstSecondChoice.SelectedIndex == -1 || ImmuneAgainstFirstChoice.SelectedIndex == -1
            || ImmuneAgainstSecondChoice.SelectedIndex == -1 || TeamNameChoice.SelectedIndex == -1)
            {
                MessageBoxResult result = MessageBox.Show("Please fill out and pick something from all boxes");
            }
            else if (parsedTP.Count < 4)
            {
                Console.WriteLine(parsedTP.Count);
                MessageBoxResult result = MessageBox.Show("Please fix the errors that was shown");
            }
            else
            {
                Fighter_DTO pDTO = new Fighter_DTO(); //only needed here locally
                RetrieveInput(pDTO, parsedTP);
                foreach (var item in pImpl.GetCurrentList())
                {
                    StrongAgainstFirstChoice.Items.Add(item.NameGS);
                    StrongAgainstSecondChoice.Items.Add(item.NameGS);
                    WeakAgainstFirstChoice.Items.Add(item.NameGS);
                    WeakAgainstSecondChoice.Items.Add(item.NameGS);
                    ImmuneAgainstFirstChoice.Items.Add(item.NameGS);
                    ImmuneAgainstSecondChoice.Items.Add(item.NameGS);
                }
            }
        }
        
        /// <summary>
        /// removes slime image that covers the box where you have the opportunity to create a new team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTeamWindow_Click(object sender, RoutedEventArgs e)
        {
            CreateTeamBox.Visibility = Visibility.Visible;
            CoverTeamCanvasImage.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// creates a team based on the arguments, only works if there is a team name and a team image.
        /// The team image cannot be the same as one already existing, currently.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitTeam_Click(object sender, RoutedEventArgs e)
        {
            if (TeamNameTextBox.Text == "" || NewTeamImage.GetValue(Image.SourceProperty) == null)
            {
                MessageBoxResult result = MessageBox.Show("remember to insert a name and select an image");
            }
            else
            {
                string teamName = TeamNameTextBox.Text;
                tImpl.AddAllyTeam(teamName, TeamImageName);
                TeamNameTextBox.Clear();
                NewTeamImage.ClearValue(Image.SourceProperty); //clears the image 
                TeamNameChoice.Items.Add(teamName);
                CreateTeamBox.Visibility = Visibility.Hidden;
                CoverTeamCanvasImage.Visibility = Visibility.Visible;
            }
        }

        // sets image into team window
        /// <summary>
        /// Opens dialog box that allows you to pick an image for your team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsertImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                try
                {
                    NewTeamImage.Stretch = Stretch.Fill;
                    string endPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Sources\\";
                    File.Copy(op.FileName, endPath + Path.GetFileName(op.FileName));
                    TeamImageName = System.IO.Path.GetFileNameWithoutExtension(op.FileName);
                    NewTeamImage.Source = new BitmapImage(new Uri(op.FileName));
                    Console.WriteLine(op.FileName);
                }
                //cathes exception that handles duplicate image
                catch (Exception exc)
                {
                    if(exc is IOException)
                    {
                        try
                        {
                            throw new DuplicateImage("duplicate Image");
                        }
                        catch(DuplicateImage dpImg)
                        {
                            Console.WriteLine(dpImg.StackTrace.ToString());
                        }
                    }
                }
        }
        }

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        { /*do not close mw.*/ exitApp = false; /*loads mainWindow*/ if (w is MainWindow) { this.w.Show(); this.Close(); } /*loads any other window */ else { w = new Window(); w.Show(); this.Close(); } }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); exitApp = false; this.Close(); }
        #endregion

        #region methods
        /// <summary>
        /// Retrieves input from all input/selectable fields
        /// </summary>
        /// <param name="pDTO"> The fighter we wish to add the information to </param>
        /// <param name="parsedTP"> The list of parsed integers from strings </param>
        public void RetrieveInput(Fighter_DTO pDTO, List<int> parsedTP)
        {
            pDTO.NameGS = NameTextBox.Text;
            pDTO.HealthGS = parsedTP[0];
            pDTO.OffenceGS = parsedTP[1];
            pDTO.DefenceGS = parsedTP[2];
            pDTO.MoveGS = parsedTP[3];
            pDTO.TeamGS = TeamNameChoice.SelectedItem.ToString();
            pDTO.StrongAgainstGS.Add(StrongAgainstFirstChoice.SelectedItem.ToString());
            pDTO.StrongAgainstGS.Add(StrongAgainstSecondChoice.SelectedItem.ToString());
            pDTO.WeakAgainstGS.Add(WeakAgainstFirstChoice.SelectedItem.ToString());
            pDTO.WeakAgainstGS.Add(WeakAgainstSecondChoice.SelectedItem.ToString());
            pDTO.ImmuneAgainstGS.Add(ImmuneAgainstFirstChoice.SelectedItem.ToString());
            pDTO.ImmuneAgainstGS.Add(ImmuneAgainstSecondChoice.SelectedItem.ToString());
            pImpl.AddParticipantToList(pDTO);
            ClearFields();
        }

        /// <summary>
        /// Clears out all fields
        /// </summary>
        public void ClearFields()
        {
            NameTextBox.Clear();
            HealthTextBox.Clear();
            OffenceTextBox.Clear();
            DefenceTextBox.Clear();
            MoveTextBox.Clear();
            StrongAgainstFirstChoice.SelectedIndex = -1;
            StrongAgainstSecondChoice.SelectedIndex = -1;
            WeakAgainstFirstChoice.SelectedIndex = -1;
            WeakAgainstSecondChoice.SelectedIndex = -1;
            ImmuneAgainstFirstChoice.SelectedIndex = - 1;
            ImmuneAgainstSecondChoice.SelectedIndex = -1;
            TeamNameChoice.SelectedIndex = -1;
        }

        #endregion
    }
}