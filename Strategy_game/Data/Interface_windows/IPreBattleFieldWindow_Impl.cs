using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    interface IPreBattleFieldWindow_Impl<Z,E,T>
    {
        void CreatePreField();
        void ShowTeamList();
        void ShowTeamMemberLists(Z teamName);
        void MoveToSpot();
        void ClearsImage(E xCoord, E yCoord, Z participant_name);
        void SetsImage(E xCoord, E yCoord, Z participant_name);
        List<T> GenerateCoordsList();
    }
}