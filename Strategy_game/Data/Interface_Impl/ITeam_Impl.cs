using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface ITeam_Impl<S,F>
    {
        void AddAllyTeam(S teamName, S imageName);
        Dictionary<S, S> GetAllyTeamList();
        S GetAllyTeamImage(S teamName);
        S GetAllyTeamName();
        List<F> GetAllyTeam(S allyTeamName);
        string GetEnemyTeamName();
        List<F> GetEnemyTeam(S enemyTeamName);
        void AddEnemyTeam(S enemyTeamName, S enemyTeamImage);

    }
}
