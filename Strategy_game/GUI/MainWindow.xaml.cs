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
        Participant_DTO pDTO2;
        Participant_Impl pImpl;
        Game_Logic_Impl gImpl;
        #endregion

        #region constructors
        public MainWindow()
        {
            Application.Current.MainWindow = this;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            /**Test Section  START **/
            


            pImpl = new Participant_Impl();
            gImpl = new Game_Logic_Impl();
            for (int i = 0; i < 6; i++)
            {
                pDTO = new Participant_DTO(100, 4, 4, 2, 2, "Destroyer" + i, "Tupac", "a", "b", "c", "d", "e", "f");
                pImpl.AddToList(pDTO);
            }

            pImpl.AddTeam("Blue", "1");
            pImpl.AddEnemyTeam("Tupac", "1");
            #region skins
            //Adds possible player images.
            Storage.PlayerSkins.Add("pinkPlayer.png");
            Storage.PlayerSkins.Add("tealPlayer.png");
            Storage.PlayerSkins.Add("redPlayer.png");
            Storage.PlayerSkins.Add("greenPlayer.png");
            Storage.PlayerSkins.Add("orangePlayer.png");
            Storage.PlayerSkins.Add("yellowPlayer.png");
            #endregion

            //sets points difference from 0,0
            /*            FieldPoint_DTO field = new FieldPoint_DTO();
                        field.XPoint = 1;
                        field.YPoint = 6;
                        pDTO.PointGS = field;

                        //gImpl.AddParticipantToField(pDTO);
                        field.XPoint = 1; 
                        pDTO2.PointGS = field;
                        //gImpl.AddParticipantToField(pDTO2);
                        List<Tuple<Participant_DTO, FieldPoint_DTO>> tmp = gImpl.GetField();



                        foreach (var point in tmp)
                        {
                            Console.WriteLine(point.Item1.GetToString()); //not a method since it is defined as a property within the DTO /FieldPoint_DTO
                            //Console.WriteLine(point.Item2.ToString); // prints out the field (but the field is also within the participant
                            //only prints keys as value (possible use that sql thing to add or retrieve from a list using sql statements)
                        }
                        //Console.WriteLine(pImpl.ToString); //prints to screen
                        pImpl.AddToList(pDTO);
                        pImpl.AddToList(pDTO2);
                        */
            /**Test Section  END **/
        }
        #endregion

        #region buttons
        //Shows add user window and hides mainwindow
        //Can't close mainwindow as that will bug the app out.
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ParticipantCreateWindow pcw = new ParticipantCreateWindow(this, this); //sends mainWindow to both parameters as we are in mainwindow
            pcw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pcw.Show();
            this.Hide();
        }

        //Prints out user lists in console output.
        private void GetList_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in pImpl.GetCurrentList())
            {
                Console.WriteLine(item.GetToString());
            }
        }

        private void Field_Click(object sender, RoutedEventArgs e)
        {
            FieldWindow fw = new FieldWindow(this, this, this.gImpl);
            fw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            fw.Show();
            this.Hide();
        }

        private void PreBattlebutton_Click(object sender, RoutedEventArgs e)
        {
            PreBattleFieldWindow pbw = new PreBattleFieldWindow(this,this, gImpl, pImpl);
            pbw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pbw.Show();
            this.Hide();
        }
        #endregion
    }
}
