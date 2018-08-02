using Strategy_game.Data.Interface_DAO;
using System.Collections.Generic;

namespace Strategy_game.Data.DAO
{
    class Team_DAO : ITeam_intDAO_generic<string>
    {
        public static Dictionary<string, string> teamList = new Dictionary<string, string>();

        public void CreateTeam(string teamName, string teamImage)
        {
            teamList.Add(teamName, teamImage);
        }

        public void DeleteTeam(string teamName)
        {
            teamList.Remove(teamName);
        }

        public Dictionary<string, string> ReadTeams()
        {
            return teamList;
        }

        public void UpdateTeam(string oldTeamName, string newTeamName, string teamImage)
        {
            foreach (var item in teamList)
            {
                if (item.Key == oldTeamName) teamList.Remove(item.Key); teamList.Add(newTeamName, teamImage);
            }
        }
    }
}
