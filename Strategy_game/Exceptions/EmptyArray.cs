using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Exceptions
{
    class EmptyArray : Exception
    {
        public EmptyArray()
        {
        }

        public EmptyArray(string message) : base(message)
        {
            MessageBoxResult resu = MessageBox.Show("The enemy list is empty, please create an enemy team or add one of your own teams to the enemy list under: xxx");
        }

        public EmptyArray(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyArray(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
