using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    /**
     * super class for leg and arm, ect. 
     */
    class Part
    {
        private double health = 0;
        private String name = "";
        public double HealthModify //special type of method that gets and sets
        {
            get { return health; }
            set { if (value < 1) { /*do nothing*/ } else { health = value; } } //can just do b = value; or do more advanced code as illustrated.
        }
        public String NameModify //special type of method that gets and sets
        {
            get { return name; }
            set { name = value; } //can just do b = value; or do more advanced code as illustrated.
        }
    }
}
