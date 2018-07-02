using Strategy_game.Data.DTO;
using Strategy_game.Func;
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
        Game_Logic_Impl gli;

        public FieldWindow()
        {
            InitializeComponent();
        }

        public FieldWindow(MainWindow mw, Window w)
        {
            gli = new Game_Logic_Impl(); 
            exitApp = true; //used for closing app 
            this.mw = mw; 
            this.w = w; 
            Closed += new EventHandler(App_exit); //subscribing to closed event 
            InitializeComponent(); 

            /* Test Section START */ 

            // Inserts image into site. (not sure how the path works)
            string toop = "pack://application:,,/Strategy_game;component/Sources/SlimeBlack.png";
            Uri uri = new Uri(toop, UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img = x1y6;
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;

            for (int i = 0; i < 10; i++)
            {
                TextBlock t = new TextBlock();
                t.Text = "Participant" + i;
                ListOfParticipants.Items.Add(t);
            }
            /* Test Section END */
        }

        //Triggers when window is closed.
        void App_exit(object sender, EventArgs e) /*App_exit is my own defined method.*/ { if (exitApp == true) { w.Close(); } /*closes mainWindow*/ }

        private void XCoord_TextChanged(object sender, TextChangedEventArgs e) { if (xCoord.Text.Length > 0) HintXCoord.Visibility = Visibility.Hidden; else HintXCoord.Visibility = Visibility.Visible; }

        private void YCoord_TextChanged(object sender, TextChangedEventArgs e) { if (yCoord.Text.Length > 0) HintYCoord.Visibility = Visibility.Hidden; else HintYCoord.Visibility = Visibility.Visible; }

        // Accesses the previous window
        private void ToPreviousWindow_Click(object sender, RoutedEventArgs e)
        { /*do not close mw.*/ exitApp = false; /*loads mainWindow*/ if (w is MainWindow) { this.w.Show(); this.Close(); } /*loads any other window */ else { w = new Window(); w.Show(); this.Close(); } }

        //Loads mainwindow
        private void ToMenuWindow_Click(object sender, RoutedEventArgs e) { mw.Show(); exitApp = false; this.Close(); }

        private void SubmitMove_Button_Click(object sender, RoutedEventArgs e)
        {
            if(xCoord.Text == "" || yCoord.Text == "")
            { Console.WriteLine("The boxes was empty"); }
            else
            {
                MoveToSpot();

                xCoord.Clear();
                yCoord.Clear();
                PlayingDisplayBox.Background = Brushes.Green;
            }
        }

        //Moves participant in fieldDTO List
        public void MoveToSpot() 
        {
            Image img = new Image();
            int x = int.Parse(xCoord.Text);
            int y = int.Parse(yCoord.Text);
            
            string participantToMove = ListOfParticipants.SelectedItem.ToString();
            string fieldCoord = gli.GetParticipantFieldCoord(participantToMove);
            Console.WriteLine(fieldCoord);
            img = (Image)FieldGrid.FindName(fieldCoord);


            img.ClearValue(Image.SourceProperty); //clears the image //problem with null object 
            //moves participant in storage and on field list
            gli.MoveParticipant(x, y, participantToMove); //Updates participantDTO in storage 

            //gets image from participant to move.
            string image = gli.GetImage(participantToMove);
            string uploadImage = image;
            //prepares the image
            Uri uri = new Uri(uploadImage, UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(uri);
            
            //finds the image field based on the coords
            string fieldName = "x" + x +"y" + y;
            img = (Image) FieldGrid.FindName(fieldName);

            img.Stretch = Stretch.Fill; 
            img.Source = bitmap; 

            //TODO Change field DTO list 
            //DELETE picture from original spot 
        }
        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
    } 
} 