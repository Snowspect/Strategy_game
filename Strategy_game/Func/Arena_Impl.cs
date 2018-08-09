using Strategy_game.Data;
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
    public class Arena_Impl : IArena_Impl<int, Participant_DTO, ArenaFieldPoint_DTO>
    {
        Arena_DTO A_DTO;
        FieldPoint_Impl fImpl;
        public Arena_Impl()
        {
            fImpl = new FieldPoint_Impl();
        }

        public void AddParticipantToField(Participant_DTO pDTO)
        {
            pDTO.PointGS = fImpl.GetArenaField(pDTO.PointGS.XPoint, pDTO.PointGS.YPoint); //Adds correct point object to participant
            pDTO.PointGS.PDTO = pDTO; //Adds participant to Arena
        }

        public void AddPointToField(ArenaFieldPoint_DTO fpDTO)
        {
            Arena_DTO.field.Add(fpDTO);
        }

        public void CreateFullArena()
        {
            int height = 6;
            int length = 6;

            for (int y = height; y > 0; y--) //x is 6, decreased to 1
            {
                for (int x = 1; x < length + 1; x++) // y is 1, increased to 6 
                {
                    ArenaFieldPoint_DTO AFP_DTO = new ArenaFieldPoint_DTO();
                    AFP_DTO.XPoint = x;
                    AFP_DTO.YPoint = y;
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.notOccupied;
                    AddPointToField(AFP_DTO);
                }
            }
            Arena_Impl tmp1 = new Arena_Impl();
            tmp1.GetField();
        }


        /// <summary>
        /// Removes a warrior from their teams list (not sure i will need it)
        /// </summary>
        /// <param name="pDTO"></param>
        public void DeleteFighterFromArena(Participant_DTO pDTO)
        {
            if (pDTO.TeamColorGS.Equals("purple"))
            {
                Arena_DTO.AllianceTeam.Remove(pDTO);
                Arena_DTO.ActiveFighters.Remove(pDTO);
            }
            else
            {
                Arena_DTO.HordeTeam.Remove(pDTO);
                Arena_DTO.ActiveFighters.Remove(pDTO);
            }
        }

        public void EmptyField()
        {
            Arena_DTO.field.Clear();
        }
        public List<ArenaFieldPoint_DTO> GetField()
        {
            return Arena_DTO.field;
        }
        //Checks for what is on the field at this given time.
        public bool CheckField(ArenaFieldPoint_DTO AFP_DTO_param, Participant_DTO pDTO)
        {
            MessageBoxResult res;
            foreach (ArenaFieldPoint_DTO AFP_DTO in Arena_DTO.field)
            {
                if (AFP_DTO == AFP_DTO_param)
                {
                    if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.notOccupied))
                    {
                        //no need to display message here as we are simply moving to a field that doesn't matter
                        //res = MessageBox.Show("You have moved");
                        return true;
                    }
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
                            
                            //not removing players here as they are overwritten when another player gets allowed to on to their field
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
                            //no reason to show message here as you are moving to a self owned field
                            //res = MessageBox.Show("Moved to Alliance field");
                            return true;
                        }
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.HordeOccupied))
                    {
                        //in case either ally or enemy attempts to move to that spot
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
                            //res = MessageBox.Show("Moved to horde field");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
