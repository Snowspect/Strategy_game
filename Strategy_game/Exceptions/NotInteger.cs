using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Exceptions
{
    class NotInteger : Exception
    {

        public NotInteger()
        {

        }

        public NotInteger(string message) : base(message)
        {
            MessageBox.Show(message);
        }

        public NotInteger(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected NotInteger(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
