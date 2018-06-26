﻿using Strategy_game.Data;
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
        public MainWindow()
        {
            InitializeComponent();
            Participant_DTO pDTO = new Participant_DTO(100, 4, 4, 2, 2, "Destroyer");
            Participant_DTO pDTO2 = new Participant_DTO(100, 4, 4, 2, 2, "Cooker");
            Participant_Impl pImpl = new Participant_Impl(); //should not be neccesary, the DTO should have a constructor which sets its values
            //Console.WriteLine(pImpl.ToString); //prints to screen
            pImpl.AddToList(pDTO);
            pImpl.AddToList(pDTO2);

            foreach (var item in pImpl.GetCurrentList())
            {
                Console.WriteLine(item.ToString);
            }
        }

        //Shows add user window and hides mainwindow
        //Can't close mainwindow as that will bug the app out.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ParticipantCreateWindow pcw = new ParticipantCreateWindow(this, this); //sends mainWindow to both parameters as we are in mainwindow
            pcw.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            pcw.Show();
            this.Hide();
        }
    }
}
