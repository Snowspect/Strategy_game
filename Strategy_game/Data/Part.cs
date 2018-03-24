using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    class Part
    {
        private double health = 0;
        private String name = "";
        public double HealthModify //special type of method that gets and sets
        {
            get { return health; }
            set { health = value; } //can just do b = value; or do more advanced code as illustrated.
        }
        public String NameModify //special type of method that gets and sets
        {
            get { return name; }
            set { name = value; } //can just do b = value; or do more advanced code as illustrated.
        }
    }
}
