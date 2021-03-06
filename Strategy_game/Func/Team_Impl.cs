﻿using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Data.Interface_Impl;
using System.Collections.Generic;

namespace Strategy_game.Func
{
    public class Team_Impl : ITeam_Impl<string, Fighter_DTO>
    {
        Team_DAO team;
        public Team_Impl(Team_DAO team)
        {
            this.team = team;
        }
        public void AddAllyTeam(string teamName, string imageName)
        {
            team.CreateTeam(teamName, imageName);
        }

        public Dictionary<string, string> GetAllyTeamList()
        {
            return team.ReadTeams();
        }
        public string GetAllyTeamImage(string teamName)
        {
            return team.GetTeamImage(teamName);
        }
        public string GetAllyTeamName()
        {
            return team.GetAllyTeam();
        }
        public List<Fighter_DTO> GetAllyTeam(string allyTeamName)
        {
            return team.GetAllyTeamList(allyTeamName);
        }

        public string GetEnemyTeamName()
        {
            return team.GetEnemyTeam();
        }
        public List<Fighter_DTO> GetEnemyTeam(string enemyTeamName)
        {
            return team.GetEnemyTeamList(enemyTeamName);
        }
        public void AddEnemyTeam(string enemyTeamName, string enemyTeamImage)
        {
            team.CreateEnemyTeam(enemyTeamName, enemyTeamImage);
        }
    }
}
