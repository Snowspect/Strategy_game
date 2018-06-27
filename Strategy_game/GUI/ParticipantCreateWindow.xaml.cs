using Strategy_game.Data;
using Strategy_game.Func;
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

        //triggered when Text Changes within name box (all hide or show the hint box)
        private void NameTextChanged(object sender, TextChangedEventArgs e) { if (NameTextBox.Text.Length >0) HintName.Visibility = Visibility.Hidden; else HintName.Visibility = Visibility.Visible; }

        private void HealthTextBox_TextChanged(object sender, TextChangedEventArgs e) { if(HealthTextBox.Text.Length > 0) HintHealth.Visibility = Visibility.Hidden; else HintHealth.Visibility = Visibility.Visible; }

        private void OffenceTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (OffenceTextBox.Text.Length > 0) HintOffence.Visibility = Visibility.Hidden; else HintOffence.Visibility = Visibility.Visible; }

        private void DefenceTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (DefenceTextBox.Text.Length > 0) HintDefence.Visibility = Visibility.Hidden; else HintDefence.Visibility = Visibility.Visible; }

        private void HMoveTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (HMoveTextBox.Text.Length > 0) HintHMove.Visibility = Visibility.Hidden; else HintHMove.Visibility = Visibility.Visible; }

        private void VMoveTextBox_TextChanged(object sender, TextChangedEventArgs e) { if (VMoveTextBox.Text.Length > 0) HintVMove.Visibility = Visibility.Hidden; else HintVMove.Visibility = Visibility.Visible; }

        //retrieves content from boxes and adds to a DTO directly. 
        private void SubmitParticipant_Click(object sender, RoutedEventArgs e)
        {
            Participant_DTO pDTO = new Participant_DTO();
            Participant_Impl pImpl = new Participant_Impl();

            pDTO.NameGS = NameTextBox.Text;
            pDTO.HealthGS = int.Parse(HealthTextBox.Text);
            pDTO.OffenceGS = int.Parse(OffenceTextBox.Text);
            pDTO.DefenceGS = int.Parse(DefenceTextBox.Text);
            pDTO.HMoveGS = int.Parse(HMoveTextBox.Text);
            pDTO.VMoveGS = int.Parse(VMoveTextBox.Text);
            pImpl.AddToList(pDTO);
        }
    }
}