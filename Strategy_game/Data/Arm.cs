using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    class Arm : Part
    {
        private int reach; //polymorfi
        private int hitVerticaldmg, hitHorizontaldmg; //polymorfi


        public Arm()
        {
            this.reach = 0;
            this.hitHorizontaldmg = 0;
            this.hitVerticaldmg = 0;
            this.HealthModify = 0;
            this.NameModify = "john doe";
        }

        public int ReachModify //special type of method that gets and sets
        {
            get { return reach; }
            set { if (value < 1) { /*do nothing*/ } else { reach = value; } } //can just do b = value; or do more advanced code as illustrated.
        }
        public int HitVerticalModify //special type of method that gets and sets
        {
            get { return hitVerticaldmg; }
            set { if (value < 1) { /*do nothing*/ } else { hitVerticaldmg = value; } } //can just do b = value; or do more advanced code as illustrated.
        }
        public int HitHorizontalModify //special type of method that gets and sets
        {
            get { return hitHorizontaldmg; }
            set { if (value < 1) { /*do nothing*/ } else { hitHorizontaldmg = value; } } //can just do b = value; or do more advanced code as illustrated.
        }
    }
}
