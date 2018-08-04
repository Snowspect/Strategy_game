using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface ITeam_Impl<Z,P>
    {
        //ally team
        void AddAllyTeam(Z teamName, Z imageName);
        Dictionary<Z, Z> GetAllyTeamList();
        Z GetAllyTeamImage(Z teamName);

        //enemy team
        Z GetEnemyTeamName();
        List<P> GetEnemyTeam(Z enemyTeamName);
        void AddEnemyTeam(Z enemyTeamName, Z enemyTeamImage);
    }
}
