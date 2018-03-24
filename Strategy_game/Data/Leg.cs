using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    class Leg : Part
    {
        private int movementHorizontal, movementVertical;
        public int MovementHorizontalModify //special type of method that gets and sets
        {
            get { return movementHorizontal; }
            set { movementHorizontal = value; } //can just do b = value; or do more advanced code as illustrated.
        }
        public int MovementVerticalModify //special type of method that gets and sets
        {
            get { return movementVertical; }
            set { movementVertical = value; } //can just do b = value; or do more advanced code as illustrated.
        }
    }
}
