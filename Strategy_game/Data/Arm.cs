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
        private int hitVertial, hitHorizontal; //polymorfi
        public int ReachModify //special type of method that gets and sets
        {
            get { return reach; }
            set { reach = value; } //can just do b = value; or do more advanced code as illustrated.
        }
        public int HitDir1Modify //special type of method that gets and sets
        {
            get { return hitVertial; }
            set { hitVertial = value; } //can just do b = value; or do more advanced code as illustrated.
        }
        public int HitDir2Modify //special type of method that gets and sets
        {
            get { return hitHorizontal; }
            set { hitHorizontal = value; } //can just do b = value; or do more advanced code as illustrated.
        }
    }
}
