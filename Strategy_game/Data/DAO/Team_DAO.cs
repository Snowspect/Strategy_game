using Strategy_game.Data.Interface_DAO;
using System.Collections.Generic;
using System;
using System.Windows;

namespace Strategy_game.Data.DAO
{
    public class Team_DAO : ITeam_intDAO_generic<string, Fighter_DTO>
    {
        #region allyTeamMethods
        public void CreateTeam(string teamName, string teamImage)
        {
            Storage.teamList.Add(teamName, teamImage);
        }

        public void DeleteTeam(string teamName)
        {
            Storage.teamList.Remove(teamName);
        }

        public Dictionary<string, string> ReadTeams()
        {
            return Storage.teamList;
        }

        public void UpdateTeam(string oldTeamName, string newTeamName, string teamImage)
        {
            foreach (var item in Storage.teamList)
            {
                if (item.Key == oldTeamName) Storage.teamList.Remove(item.Key); Storage.teamList.Add(newTeamName, teamImage);
            }
        }

        public string GetTeam()
        {
            throw new NotImplementedException();
        }

        public string GetTeamImage(string teamName)
        {
            Storage.teamList.TryGetValue(teamName, out string result);
            return result;
        }
        #endregion

        #region EnemyTeamMethods
        public void CreateEnemyTeam(string teamName, string teamImage)
        {
            Storage.enemyTeamList.Add(teamName, teamImage);
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
            int limit = Storage.teamList.Count;
            Random rand = new Random();
            string allyTeam = "";
            List<string> values = new List<string>();
            foreach (var item in Storage.teamList.Keys)
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
            int limit = Storage.enemyTeamList.Count;
            Random rand = new Random();
            string enemyTeam = "";
            List<string> values = new List<string>();
            foreach (var item in Storage.enemyTeamList.Keys)
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

        public List<Fighter_DTO> GetEnemyTeamList(string enemyTeamName)
        {
            List<Fighter_DTO> tmp = new List<Fighter_DTO>();
            foreach (var item in Storage.StParticipant)
            {
                if(item.TeamGS.Equals(enemyTeamName))
                {
                    tmp.Add(item);
                }
            }
            return tmp;
        }
        public List<Fighter_DTO> GetAllyTeamList(string allyTeamName)
        {
            List<Fighter_DTO> tmp = new List<Fighter_DTO>();
            foreach (Fighter_DTO pDTO in Storage.StParticipant)
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
