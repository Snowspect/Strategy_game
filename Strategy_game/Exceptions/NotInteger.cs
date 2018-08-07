using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Exceptions
{
    class NotInteger : Exception
    {
        public NotInteger(string message) : base(message)
        {
            MessageBox.Show(message);
        }
    }
}
