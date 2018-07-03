using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    interface IFieldWindow_Impl<S,Z>
    {
        void MoveToSpot();
        void ClearsImage(S xCoord, S yCoord, Z participant_name);
        void SetsImage(S xCoord, S yCoord, Z participant_name);
    }
}
