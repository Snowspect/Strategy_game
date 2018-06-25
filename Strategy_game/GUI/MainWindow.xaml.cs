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
            Console.WriteLine("helloooo");

            InitializeComponent();
            Participant_DTO pDTO = new Participant_DTO();
            Participant_Impl pImpl = new Participant_Impl(pDTO);
            Console.WriteLine(pImpl.ToString); //
        }
    }
}
