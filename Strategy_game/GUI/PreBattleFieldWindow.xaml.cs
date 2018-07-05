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
using System.Xml;
using System.Xml.Linq;

namespace Strategy_game.GUI
{
    /// <summary>
    /// Interaction logic for PreBattleFieldWindow.xaml
    /// </summary>
    public partial class PreBattleFieldWindow : Window
    {
        public PreBattleFieldWindow()
        {
            string XmlDocu = new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\GUI\\" + "PreBattleFieldWindow.xaml").ToString();
            XDocument xdoc = XDocument.Load(XmlDocu);
            Console.WriteLine(xdoc);
            var item = xdoc.Element("FieldSizeBattle");
            int j = 6;
            int i = FieldSizeBattle.Columns = 3;
            int h = 0;
            //int size = 6 * 3;

            for (int k = 1; k < j + 1; k++) //k is 1, increased to 6
            {
                h = j;
                for (int g = 1; g < i + 1; g++) // g is 1, increased to 3 
                {
                    string xName = "x" + g + "y" + h--; //g goes 1,2,3...  
                    item.Add("<Border BorderThickness=\"1\" BorderBrush=\"black\"><Image x:Name=" + xName + "></Image></Border>");
                }
            }


//            item.Add("<Image x:Name=" + xName + "></Image>");
             
            InitializeComponent();
        }
    }
}
