using Strategy_game.Data.Interface_DAO;
using System.Collections.Generic;
using System;
using System.Windows;

namespace Strategy_game.Data.DAO
{
    class Team_DAO : ITeam_intDAO_generic<string>
    {
        public static Dictionary<string, string> teamList = new Dictionary<string, string>();
        public static Dictionary<string, string> enemyTeamList = new Dictionary<string, string>();

        #region allyTeamMethods
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

        public string GetTeam()
        {
            throw new NotImplementedException();
        }

        public string GetTeamImage(string teamName)
        {
            teamList.TryGetValue(teamName, out string result);
            return result;
        }
        #endregion

        #region EnemyTeamMethods
        public void CreateEnemyTeam(string teamName, string teamImage)
        {
            enemyTeamList.Add(teamName, teamImage);
        }

        public void UpdateEnemyTeam(string oldTeamName, string newTeamName, string teamImage)
        {
            throw new NotImplementedException(); 
        }

        public void DeleteEnemyTeam(string teamName)
        {
            throw new NotImplementedException();
        }

        public string GetAllyTeam()
        {
            int limit = teamList.Count;
            Random rand = new Random();
            string allyTeam = "";
            List<string> values = new List<string>();
            foreach (var item in teamList.Keys)
            {
                values.Add(item);
            }
            try
            {
                if (limit != 0) limit = limit - 1; //to make sure we don't get 0 to -1 range
                allyTeam = values[rand.Next(0, limit)];
            }
            catch (Exception exec)
            {
                Console.WriteLine(exec.StackTrace.ToString());
                Console.WriteLine(exec);
                MessageBoxResult result = MessageBox.Show("The ally Team List is empty");
            }
            return allyTeam;
        }

        public string GetEnemyTeam()
        {
            int limit = enemyTeamList.Count;
            Random rand = new Random();
            string enemyTeam = "";
            List<string> values = new List<string>();
            foreach (var item in enemyTeamList.Keys)
            {
                    values.Add(item);   
            }
            try
            {
                if (limit != 0) limit = limit - 1; //to make sure we don't get 0 to -1 range
                enemyTeam = values[rand.Next(0, limit)];
            }
            catch (Exception exec)
            {
            Console.WriteLine(exec.StackTrace.ToString());
            Console.WriteLine(exec);
            MessageBoxResult result = MessageBox.Show("The enemy Team List is empty");
            }
            return enemyTeam;
        }

        public string GetEnemyTeamImage(string teamName)
        {
            throw new NotImplementedException();
        }

        public List<Participant_DTO> GetEnemyTeamList(string enemyTeamName)
        {
            List<Participant_DTO> tmp = new List<Participant_DTO>();
            foreach (var item in Storage.StParticipant)
            {
                if(item.TeamGS.Equals(enemyTeamName))
                {
                    tmp.Add(item);
                }
            }
            return tmp;
        }
        public List<Participant_DTO> GetAllyTeamList(string allyTeamName)
        {
            List<Participant_DTO> tmp = new List<Participant_DTO>();
            foreach (Participant_DTO pDTO in Storage.StParticipant)
            {
                if (pDTO.TeamGS.Equals(allyTeamName))
                {
                    tmp.Add(pDTO);
                }
            }
            return tmp;
        }
        #endregion
    }
}
