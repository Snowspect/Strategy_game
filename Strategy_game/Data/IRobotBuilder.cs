using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    interface IRobotBuilder
    {
        void BuildRobot(Arm a, Leg l, int shield, String name);
        Part GetInfo();
    }
}
