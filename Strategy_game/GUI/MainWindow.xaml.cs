using Strategy_game.Data;
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
        Game_Logic_Impl gImpl;
        Team_Impl tImpl;
        #endregion

        #region constructors
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            pImpl = new Participant_Impl();
            tImpl = new Team_Impl();
            gImpl = new Game_Logic_Impl();
            //Creates an enemy team of 6
            for (int i = 0; i < 6; i++)
            {
                pDTO = new Participant_DTO(100, 4, 4, 2, 2, "Destroyer" + i, "Tupac", "a", "b", "c", "d", "e", "f");
                pImpl.AddParticipantToList(pDTO);
            }
            //creates ally team and enemy team
            tImpl.AddAllyTeam("Blue", "1");
            tImpl.AddEnemyTeam("Tupac", "1");

            //add participantSkins to game
            #region skins
            //Adds possible player images.
            Storage.PlayerSkins.Add("pinkPlayer.png");
            Storage.PlayerSkins.Add("tealPlayer.png");
            Storage.PlayerSkins.Add("redPlayer.png");
            Storage.PlayerSkins.Add("greenPlayer.png");
            Storage.PlayerSkins.Add("orangePlayer.png");
            Storage.PlayerSkins.Add("yellowPlayer.png");
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

        private void Fieldbutton_Click(object sender, RoutedEventArgs e)
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
