using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strategy_game;
using Strategy_game.Data;
using System.Collections.Generic;
using Strategy_game.Data.DTO;

namespace GameTest
{
    [TestClass]
    public class ArenaTest
    {

        Strategy_game.Data.DTO.Arena_DTO A_DTO;
        Strategy_game.Data.DAO.Arena_DAO A_DAO;
        Strategy_game.Func.Arena_Impl arenaImpl;
        Strategy_game.Func.Fighter_Impl fImpl;
        Strategy_game.Func.Team_Impl tImpl;
        Strategy_game.Data.DAO.Team_DAO tDAO;
        Strategy_game.Data.Participant_DAO pDAO;
        MainWindow mw = new Strategy_game.MainWindow();

        public ArenaTest()
        {
            pDAO = new Strategy_game.Data.Participant_DAO();
            tDAO = new Strategy_game.Data.DAO.Team_DAO();
            A_DTO = new Strategy_game.Data.DTO.Arena_DTO();
            A_DAO = new Strategy_game.Data.DAO.Arena_DAO(A_DTO);
            arenaImpl = new Strategy_game.Func.Arena_Impl(A_DAO);
            fImpl = new Strategy_game.Func.Fighter_Impl(pDAO, tDAO, arenaImpl);
            tImpl = new Strategy_game.Func.Team_Impl(tDAO);
        }

        //[TestMethod]
        //public void ArenaImpl_coverage_test()
        //{
        //    Strategy_game.Data.DTO.ArenaFieldPoint_DTO AFP = new Strategy_game.Data.DTO.ArenaFieldPoint_DTO();
        //    arenaImpl.AddFieldToArena(AFP);
        //    arenaImpl.CheckTeamLists();
        //    arenaImpl.CreateFullArena();
        //    arenaImpl.GetArenaField(1, 1);
        //    arenaImpl.GetField();
        //    Strategy_game.Data.Fighter_DTO fDTO = new Strategy_game.Data.Fighter_DTO();
        //    fDTO.TeamColorGS = "blue";
        //    arenaImpl.AddParticipantToField(fDTO);
        //    arenaImpl.CheckField(AFP, fDTO);

        //    Strategy_game.Data.DTO.Arena_DTO.AllianceTeam.Add(fDTO);
        //    Strategy_game.Data.DTO.Arena_DTO.HordeTeam.Add(fDTO);
        //    arenaImpl.GetActivePlayers();
        //    arenaImpl.UpdateLeavingArenaFieldPoint(fDTO, "preArena");
        //    arenaImpl.UpdateLeavingArenaFieldPoint(fDTO, "hello");
        //    arenaImpl.UpdateMovingToArenaFieldStatus(AFP, "preArena");
        //    arenaImpl.UpdateMovingToArenaFieldStatus(AFP, "hello");
        //    arenaImpl.DeleteFighterFromArena(fDTO);
        //    fDTO.TeamColorGS = "purple";
        //    arenaImpl.DeleteFighterFromArena(fDTO);
        //}


        //[TestMethod]
        //public void CheckTeamImpl()
        //{
        //    tImpl.AddAllyTeam("dan", "1");
        //    tImpl.AddEnemyTeam("dan", "1");
        //    tImpl.GetAllyTeam("dan");
        //    tImpl.GetAllyTeamImage("dan");
        //    tImpl.GetAllyTeamList();
        //    tImpl.GetAllyTeamName();
        //    tImpl.GetEnemyTeam("dan");
        //    tImpl.GetEnemyTeamName();
        //}

        [TestMethod]
        public void CheckFighterImpl()
        {
            Fighter_DTO fDTO = new Fighter_DTO();
            fImpl.AddParticipantToList(fDTO);
            fImpl.AssignPicture(fDTO);
            ArenaFieldPoint_DTO AFP = new ArenaFieldPoint_DTO();
            fImpl.CheckMovement(fDTO, AFP);
            fImpl.CheckSurroundingFields(fDTO);
            fImpl.GetAllianceSkin(1);
            fImpl.GetHordeskin(1);
            fImpl.getImageFromParticipant(fDTO);
            fImpl.GetMovementRange(fDTO);
            fImpl.GetParticipant("d");
            fImpl.MoveParticipant(fDTO, AFP);
            fImpl.UpdateFieldToParticipant(fDTO);
        }
    }
}
