using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    public interface IPreBattleFieldWindow_Impl<S,E,F>
    {
        void StartBattle();
        void CreatePreArena();
        void ShowTeamList();
        void ShowTeamMemberLists(S team);
        void ClearsImage(F pDTO);
        void SetsImage(F pDTO);
        List<E> GenerateCoordsList(S arena);
    }
}