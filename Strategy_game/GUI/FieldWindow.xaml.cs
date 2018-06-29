using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for FieldWindow.xaml
    /// </summary>
    public partial class FieldWindow : Window
    {
        private MainWindow mw;
        private Window w;
        private Boolean exitApp;

        public FieldWindow()
        {
            InitializeComponent();
        }

        public FieldWindow(MainWindow mw, Window w)
        {
            exitApp = true; //used for closing app
            this.mw = mw;
            this.w = w;
            Closed += new EventHandler(App_exit); //subscribing to closed event
            InitializeComponent();

            // Inserts image into site. (not sure how the path works)
            string toop = "pack://application:,,/Strategy_game;component/Sources/SlimeBlack.png";
            Uri uri = new Uri(toop, UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img = x1y6;
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
        }

        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) { w.Close(); } /*closes mainWindow*/ }
    }
}