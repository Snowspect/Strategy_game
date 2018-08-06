using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    public interface IPreBattleFieldWindow_Impl<S,FP,P>
    {
        void CreatePreArena();
        void ShowTeamList();
        void ShowTeamMemberLists(S teamName);
        void ClearsImage(P pDTO);
        void SetsImage(P pDTO);
        List<FP> GenerateCoordsList();
    }
}