using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    interface IPreBattleFieldWindow_Impl<S,FP,P>
    {
        void CreatePreField();
        void ShowTeamList();
        void ShowTeamMemberLists(S teamName);
        void MoveToSpot();
        void ClearsImage(P participant_name);
        void SetsImage(P participant_name);
        List<FP> GenerateCoordsList();
    }
}