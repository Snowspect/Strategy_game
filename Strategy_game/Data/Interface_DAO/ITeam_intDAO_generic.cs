using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface ITeam_intDAO_generic<S, F>
    {
        void CreateTeam(S teamName, S teamImage);
        Dictionary<S,S> ReadTeams();
        void UpdateTeam(S oldTeamName, S newTeamName, S teamImage);
        void DeleteTeam(S teamName);
        S GetTeam();
        S GetTeamImage(S teamName);
        void CreateEnemyTeam(S teamName, S teamImage);
        void UpdateEnemyTeam(S oldTeamName, S newTeamName, S teamImage);
        void DeleteEnemyTeam(S teamName);
        S GetEnemyTeam();
        S GetEnemyTeamImage(S teamName);
        S GetAllyTeam();
        List<F> GetEnemyTeamList(S enemyTeamName);
        List<F> GetAllyTeamList(S allyTeamName);
    }
}
