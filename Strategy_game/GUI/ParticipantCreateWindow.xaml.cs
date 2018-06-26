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
using System.Windows.Shapes;

namespace Strategy_game.GUI
{
    /// <summary>
    /// Interaction logic for ParticipantCreateWindow.xaml
    /// </summary>
    public partial class ParticipantCreateWindow : Window
    {
        MainWindow mw;
        Window w;

        public ParticipantCreateWindow()
        {
            InitializeComponent();
        }
        //Constructor to take two kinds of windows
        public ParticipantCreateWindow(MainWindow mw, Window w)
        {
            this.w = w;
            this.mw = mw;
            InitializeComponent();
        }

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        {
            //loads mainWindow
            if(w is MainWindow) { this.w.Show(); this.Close(); }
            //loads any other window
            else { w = new Window(); w.Show(); this.Close(); }
        }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); this.Close(); }

        //triggered when Text Changes within name box
        private void NameTextChanged(object sender, TextChangedEventArgs e) { if (NameTextBox.Text.Length >0) HintName.Visibility = Visibility.Hidden; else HintName.Visibility = Visibility.Visible; }

        private void HealthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OffenceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DefenceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HMoveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void VMoveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}