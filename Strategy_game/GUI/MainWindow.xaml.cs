﻿using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Data.DTO;
using Strategy_game.Func;
using Strategy_game.GUI;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;    

namespace Strategy_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region localVariables
        Participant_DTO pDTO;
        Participant_Impl pImpl;
        
        Team_Impl tImpl;
        #endregion

        #region constructors
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            pImpl = new Participant_Impl();
            tImpl = new Team_Impl();

            String[] colors =
            {
                "PINK",
                "TEAL",
                "RED",
                "GREEN",
                "ORANGE",
                "YELLOW"
            };

            //Creates an enemy team of 6
            tImpl.AddEnemyTeam("Tupac", "1");
            for (int i = 0; i < 6; i++)
            {
                pDTO = new Participant_DTO(100, 4, 4, 2, "Horde" + colors[i], "Tupac", "a", "b", "c", "d", "e", "f");
                pImpl.AddParticipantToList(pDTO);
            }
            //creates ally team and enemy team
            tImpl.AddAllyTeam("Wolf", "1");
            for (int i = 0; i < 6; i++)
            {
                pDTO = new Participant_DTO(100, 4, 4, 2, "Alliance" + colors[i], "Wolf", "a", "b", "c", "d", "e", "f");
                pImpl.AddParticipantToList(pDTO);
            }

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
         * GetList (Prints out all participants in console.. for debugging))
         * Field_Click (access fieldWindow)
         * PreBattlebutton (access prefield window)
         */
        #region buttons
        private void CreateParticipantbutton_Click(object sender, RoutedEventArgs e)
        {
            ParticipantCreateWindow pcw = new ParticipantCreateWindow(this, this); //sends mainWindow to both parameters as we are in mainwindow
            pcw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pcw.Show();
            this.Hide();
        }

        //Prints out user lists in console output.
        private void GetListbutton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in pImpl.GetCurrentList())
            {
                Console.WriteLine(item.GetToString());
            }
        }

        private void PreBattlebutton_Click(object sender, RoutedEventArgs e)
        {
            PreBattleFieldWindow pbw = new PreBattleFieldWindow(this,this, pImpl);
            pbw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pbw.Show();
            this.Hide();
        }
        #endregion
    }
}
