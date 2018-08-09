using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Func
{
    public class Arena_Impl : IArena_Impl<string, int, Fighter_DTO, ArenaFieldPoint_DTO, bool>
    {
        Arena_DAO A_DAO;
        ArenaFieldPoint_DTO AFP_DTO;

        public Arena_Impl()
        {
            A_DAO = new Arena_DAO();
        }
        public Arena_Impl(Arena_DAO A_DAO)
        {
            this.A_DAO = A_DAO;
        }

        /// <summary>
        /// overwrites the fighter object reference a point has and vise versa.
        /// </summary>
        /// <param name="pDTO"> The fighter in which its point we will manipulate </param>
        public void AddParticipantToField(Fighter_DTO pDTO)
        {
            pDTO.PointGS = GetArenaField(pDTO.PointGS.XPoint, pDTO.PointGS.YPoint); //Adds correct point object to participant
            pDTO.PointGS.PDTO = pDTO; //Adds participant to Arena
        }

        /// <summary>
        /// Adds a arena field to the arena
        /// </summary>
        /// <param name="fpDTO"></param>
        public void AddFieldToArena(ArenaFieldPoint_DTO fpDTO)
        {
            A_DAO.AddFieldToArena(fpDTO);
        }

        /// <summary>
        /// Creates a 6x6 arena filled with ArenaFieldPoints
        /// </summary>
        public void CreateFullArena()
        {
            int height = 6;
            int length = 6;

            for (int y = height; y > 0; y--) //x is 6, decreased to 1
            {
                for (int x = 1; x < length + 1; x++) // y is 1, increased to 6 
                {
                    AFP_DTO = new ArenaFieldPoint_DTO();
                    AFP_DTO.XPoint = x;
                    AFP_DTO.YPoint = y;
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.notOccupied;
                    AddFieldToArena(AFP_DTO);
                }
            }
        }

        /// <summary>
        /// Removes a warrior from their teams list (not sure i will need it)
        /// </summary>
        /// <param name="pDTO"></param>
        public void DeleteFighterFromArena(Fighter_DTO pDTO)
        {
            if (pDTO.TeamColorGS.Equals("purple"))
            {
                A_DAO.RemoveAlliance(pDTO);
            }
            else
            {
                A_DAO.RemoveHorde(pDTO);
            }
        }

        /// <summary>
        /// Empties the arena, that is, removes all elements in our list of fields
        /// </summary>
        public void EmptyField()
        {
            A_DAO.EmptyField();
        }

        /// <summary>
        /// retrieves the entire list of arena fields
        /// </summary>
        /// <returns></returns>
        public List<ArenaFieldPoint_DTO> GetField()
        {
            return A_DAO.GetField();
        }

        /// <summary>
        /// Checks the field we are attempting to move to
        /// </summary>
        /// <param name="AFP_DTO_param"></param>
        /// <param name="pDTO"></param>
        /// <returns></returns>
        public bool CheckField(ArenaFieldPoint_DTO AFP_DTO_param, Fighter_DTO pDTO)
        {
            MessageBoxResult res;
            foreach (ArenaFieldPoint_DTO AFP_DTO in Arena_DTO.field)
            {
                if (AFP_DTO == AFP_DTO_param)
                {
                    if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.notOccupied)) { return true; }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.AllianceOccupied))
                    {
                        if (pDTO.TeamColorGS.Equals("purple"))
                        {
                            res = MessageBox.Show("An Alliance member is here");
                            return false;
                        }
                        else
                        {
                            res = MessageBox.Show("The Horde has killed an Alliance player");
                            DeleteFighterFromArena(AFP_DTO.PDTO);
                            return true;
                        }
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.AllianceOwned))
                    {
                        if (pDTO.TeamColorGS.Equals("blue"))
                        {
                            res = MessageBox.Show("You are getting a debuff by moving to an Alliance field");
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.HordeOccupied))
                    {
                        if (pDTO.TeamColorGS.Equals("purple"))
                        {
                            MessageBoxResult result = MessageBox.Show("You destroyed a horde player");
                            DeleteFighterFromArena(AFP_DTO.PDTO);
                            return true;
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("A Horde member is here");
                            return false;
                        }
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.HordeOwned))
                    {
                        if (pDTO.TeamColorGS.Equals("purple"))
                        {
                            res = MessageBox.Show("You are getting a debuff by taking Alliance team spot, but only for 1 turn.. Not yet impl");
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Gets an arena field based on two coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ArenaFieldPoint_DTO GetArenaField(int x, int y)
        {
            foreach (ArenaFieldPoint_DTO fpDTO in GetField())
            {
                if (fpDTO.XPoint == x && fpDTO.YPoint == y)
                {
                    return fpDTO;
                }
            }
            return null;
        }

        /// <summary>
        /// Takes a fighter object and updates the field it is leaving.
        /// </summary>
        /// <param name="pDTO"></param>
        /// <param name="arena"></param>
        public void UpdateLeavingArenaFieldPoint(Fighter_DTO pDTO, string arena)
        {
            int xComingFrom = pDTO.PointGS.XPoint;
            int yComingFrom = pDTO.PointGS.YPoint;
            if (xComingFrom != 0 && yComingFrom != 0)
            {
                if (!arena.Equals("preArena"))// this is for the actual arena fight, so we actually own fields we have left.
                {
                    ArenaFieldPoint_DTO AFP_DTO = GetArenaField(xComingFrom, yComingFrom);
                    if (AFP_DTO.PDTO != null)
                    {
                        if (AFP_DTO.PDTO.TeamColorGS.Equals("blue"))
                        {
                            AFP_DTO.PDTO = null;
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.HordeOwned;
                        }
                        else if (AFP_DTO.PDTO.TeamColorGS.Equals("purple"))
                        {
                            AFP_DTO.PDTO = null;
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.AllianceOwned;
                        }
                    }
                }
                else //So we don't occupy fields in the preArena window, but simply move our players around
                {
                    ArenaFieldPoint_DTO AFP_DTO = GetArenaField(xComingFrom, yComingFrom);
                    if (AFP_DTO.PDTO != null)
                    {
                        AFP_DTO.PDTO = null;

                        if (AFP_DTO.FieldPointStatusGS == FieldStatus_DTO.FieldStatus.AllianceOccupied)
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.AllianceOwned;
                        else
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.HordeOwned;
                    }
                }
            }
        }
        //When you move to a field, overwrite it's status so that you occupy it
        /// <summary>
        /// takes a fieldPoint in the arena and updates the field status based on the fighter on it
        /// </summary>
        /// <param name="AFP_DTO"></param>
        /// <param name="arena"></param>
        public void UpdateMovingToArenaFieldStatus(ArenaFieldPoint_DTO AFP_DTO, string arena)
        {
            if (AFP_DTO.PDTO != null)
            {
                if (AFP_DTO.PDTO.TeamColorGS.Equals("Blue"))
                {
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.HordeOccupied;
                }
                else if (AFP_DTO.PDTO.TeamColorGS.Equals("purple"))
                {
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.AllianceOccupied;
                }
            }
        }

        public List<Fighter_DTO> GetActivePlayers()
        {
            return A_DAO.GetActivePlayers();
        }
        public void SetActivePlayers(List<Fighter_DTO> ActivePlayers)
        {
            A_DAO.SetActivePlayers(ActivePlayers);
        }
        public bool CheckTeamLists()
        {
            return A_DAO.CheckTeamLists();
        }
    }
}
