using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Exceptions
{
    class DuplicateImage : Exception
    {
        public DuplicateImage()
        {
        }

        public DuplicateImage(string message) : base(message)
        {
            MessageBox.Show(message);
            Console.WriteLine(message);
        }

        public DuplicateImage(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateImage(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
