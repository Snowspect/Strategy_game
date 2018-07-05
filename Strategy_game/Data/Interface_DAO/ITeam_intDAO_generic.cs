using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface ITeam_intDAO_generic<S>
    {
        void CreateTeam(S teamName, S teamImage);
        Dictionary<S,S> ReadTeams();
        void UpdateTeam(S oldTeamName, S newTeamName, S teamImage);
        void DeleteTeam(S teamName);
    }
}
