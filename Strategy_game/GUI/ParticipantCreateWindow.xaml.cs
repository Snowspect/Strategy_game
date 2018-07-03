using Strategy_game.Data;
using Strategy_game.Exceptions;
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
        private MainWindow mw;
        private Window w;
        private Boolean exitApp;

        public ParticipantCreateWindow()
        {
            InitializeComponent();
        }
        //Constructor to take two kinds of windows
        public ParticipantCreateWindow(MainWindow mw, Window w)
        {
            exitApp = true; //used for closing app
            this.w = w;
            this.mw = mw;
            InitializeComponent();
            Closed += new EventHandler(App_exit); //subscribing to closed event
            StrongAgainstFirstChoice.Items.Add("st1");
            StrongAgainstSecondChoice.Items.Add("st2");
            WeakAgainstFirstChoice.Items.Add("wk1");
            WeakAgainstSecondChoice.Items.Add("wk2");
            ImmuneAgainstFirstChoice.Items.Add("imm1");
            ImmuneAgainstSecondChoice.Items.Add("imm2");
            TeamNameChoice.Items.Add("test");
        }

        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) { w.Close(); } /*closes mainWindow*/ }

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        { /*do not close mw.*/ exitApp = false; /*loads mainWindow*/ if (w is MainWindow) { this.w.Show(); this.Close(); } /*loads any other window */ else { w = new Window(); w.Show(); this.Close(); } }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); exitApp = false; this.Close(); }

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
            List<string> tp = new List<string>();
            tp.Add(HealthTextBox.Text);
            tp.Add(OffenceTextBox.Text);
            tp.Add(DefenceTextBox.Text);
            tp.Add(VMoveTextBox.Text);
            tp.Add(HMoveTextBox.Text);
            Participant_DTO pDTO = new Participant_DTO();
            Participant_Impl pImpl = new Participant_Impl();
            if (NameTextBox.Text == "" || HealthTextBox.Text == "" || OffenceTextBox.Text == "" || DefenceTextBox.Text == ""
            || HMoveTextBox.Text == "" || VMoveTextBox.Text == "" || StrongAgainstFirstChoice.SelectedIndex == -1 || StrongAgainstSecondChoice.SelectedIndex == -1
            || WeakAgainstFirstChoice.SelectedIndex == -1 || WeakAgainstSecondChoice.SelectedIndex == -1 || ImmuneAgainstFirstChoice.SelectedIndex == -1
            || ImmuneAgainstSecondChoice.SelectedIndex == -1 || TeamNameChoice.SelectedIndex == -1)
            {
                Console.WriteLine("Please fill out all Boxes");
            }
            else
            {
                Boolean nope = true;
                //Tests if one of the items are not an integer
                foreach (var item in tp)
                {
                    try
                    { 
                        int.Parse(item);
                    } catch(Exception exception)
                    {
                        Console.WriteLine(exception);
                        Console.WriteLine(exception.StackTrace.ToString());
                        throw new NotInteger(item.ToString());
                    }
                }
                if (nope == true) //retrieves data from window and inserts into Participant_DTO
                {
                    pDTO.NameGS = NameTextBox.Text;
                    pDTO.HealthGS = int.Parse(HealthTextBox.Text);
                    pDTO.OffenceGS = int.Parse(OffenceTextBox.Text);
                    pDTO.DefenceGS = int.Parse(DefenceTextBox.Text);
                    pDTO.HMoveGS = int.Parse(HMoveTextBox.Text);
                    pDTO.VMoveGS = int.Parse(VMoveTextBox.Text);
                    pDTO.TeamGS = TeamNameChoice.SelectedItem.ToString();
                    pDTO.StrongAgainstGS.Add(StrongAgainstFirstChoice.SelectedItem.ToString());
                    Console.WriteLine(StrongAgainstFirstChoice.SelectedItem.ToString());
                    pDTO.StrongAgainstGS.Add(StrongAgainstSecondChoice.SelectedItem.ToString());
                    pDTO.WeakAgainstGS.Add(WeakAgainstFirstChoice.SelectedItem.ToString());
                    pDTO.WeakAgainstGS.Add(WeakAgainstSecondChoice.SelectedItem.ToString());
                    pDTO.ImmuneAgainstGS.Add(ImmuneAgainstFirstChoice.SelectedItem.ToString());
                    pDTO.ImmuneAgainstGS.Add(ImmuneAgainstSecondChoice.SelectedItem.ToString());
                    pImpl.AddToList(pDTO);
                    ClearFields();
                }
            }
        }

        private void NewTeam_Button(object sender, RoutedEventArgs e)
        {
            //TODO To show canvas that let's you create a new team
        }

        //clear all fields after submitting
        public void ClearFields()
        {
            NameTextBox.Clear();
            HealthTextBox.Clear();
            OffenceTextBox.Clear();
            DefenceTextBox.Clear();
            HMoveTextBox.Clear();
            VMoveTextBox.Clear();
            StrongAgainstFirstChoice.SelectedIndex = -1;
            StrongAgainstSecondChoice.SelectedIndex = -1;
            WeakAgainstFirstChoice.SelectedIndex = -1;
            WeakAgainstSecondChoice.SelectedIndex = -1;
            ImmuneAgainstFirstChoice.SelectedIndex = - 1;
            ImmuneAgainstSecondChoice.SelectedIndex = -1;
            TeamNameChoice.SelectedIndex = -1;
        }
    }
}