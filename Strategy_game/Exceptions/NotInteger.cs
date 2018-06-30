using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Exceptions
{
    class NotInteger : Exception
    {
        public NotInteger(string message) : base(message)
        {
            Console.WriteLine("This is not a number: " + message);
        }
    }
}
