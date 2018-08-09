using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Func;
using Strategy_game.GUI;
using System;
using System.Windows;

namespace Strategy_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Fighter_Impl fighter_Impl;
        Team_Impl team_Impl;
        Arena_Impl arena_Impl;

        #region constructors
        public MainWindow()
        {
            //Data layer creation
            Team_DAO team_DB = new Team_DAO();
            Arena_DAO A_DAO = new Arena_DAO();
            Participant_DAO pDAO = new Participant_DAO();

            //Implementation layer creation
            arena_Impl = new Arena_Impl(A_DAO);
            fighter_Impl = new Fighter_Impl(pDAO,team_DB, arena_Impl);
            team_Impl = new Team_Impl(team_DB);

            //this is to prevent the gui from reaching down into the DAOS, so we hand the DAOS to the implementation and then the implementation to the GUI

            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            String[] colors =
            {
                "PINK",
                "TEAL",
                "RED",
                "GREEN",
                "ORANGE",
                "YELLOW"
            };

            #region create two teams
            Fighter_DTO pDTO = new Fighter_DTO(); //Only used within this method
            //Creates an enemy team of 6
            team_Impl.AddEnemyTeam("Horde", "1");
            for (int i = 0; i < 6; i++)
            {
                pDTO = new Fighter_DTO(100, 4, 4, 2, "HordeGrunt-" + colors[i], "Horde", "a", "b", "c", "d", "e", "f");
                fighter_Impl.AddParticipantToList(pDTO);
            }
            //creates ally team and enemy team
            team_Impl.AddAllyTeam("Alliance", "1");
            for (int i = 0; i < 6; i++)
            {
                pDTO = new Fighter_DTO(100, 4, 4, 2, "AllianceWarrior - " + colors[i], "Alliance", "a", "b", "c", "d", "e", "f");
                fighter_Impl.AddParticipantToList(pDTO);
            }
            #endregion 
            //add participantSkins to game
            #region skins
            //Adds possible player images.
            Storage.AllianceSkins.Add("AlliancePinkPlayer.png");
            Storage.AllianceSkins.Add("AllianceTealPlayer.png");
            Storage.AllianceSkins.Add("AllianceRedPlayer.png");
            Storage.AllianceSkins.Add("AllianceGreenPlayer.png");
            Storage.AllianceSkins.Add("AllianceOrangePlayer.png");
            Storage.AllianceSkins.Add("AllianceYellowPlayer.png");
            Storage.HordeSkins.Add("HordePinkPlayer.png");
            Storage.HordeSkins.Add("HordeTealPlayer.png");
            Storage.HordeSkins.Add("HordeRedPlayer.png");
            Storage.HordeSkins.Add("HordeGreenPlayer.png");
            Storage.HordeSkins.Add("HordeOrangePlayer.png");
            Storage.HordeSkins.Add("HordeYellowPlayer.png");
            #endregion
        }
        #endregion

        /*
         * Create_Click (access participantCreateWindow)
         * PreBattlebutton (access prefield window)
         */
        #region buttons
        private void CreateParticipantbutton_Click(object sender, RoutedEventArgs e)
        {
            ParticipantCreateWindow pcw = new ParticipantCreateWindow(this, this,fighter_Impl,team_Impl); //sends mainWindow to both parameters as we are in mainwindow
            pcw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pcw.Show();
            this.Hide();
        }

        private void PreBattlebutton_Click(object sender, RoutedEventArgs e)
        {
            PreBattleFieldWindow pbw = new PreBattleFieldWindow(this,fighter_Impl, arena_Impl, team_Impl);
            pbw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pbw.Show();
            this.Hide();
        }
        #endregion
    }
}
