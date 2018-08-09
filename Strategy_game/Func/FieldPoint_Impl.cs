using Strategy_game.Data;
using Strategy_game.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Func
{
    class FieldPoint_Impl
    {
        Arena_DTO fDTO;
        public FieldPoint_Impl()
        {
            fDTO = new Arena_DTO();
        }
        public ArenaFieldPoint_DTO GetArenaField(int x, int y)
        {
            foreach (ArenaFieldPoint_DTO fpDTO in fDTO.FieldGS)
            {
                if(fpDTO.XPoint == x && fpDTO.YPoint == y)
                {
                    return fpDTO;
                }
            }
            return null;
        }

        //Takes two coords, gets the field to those coords
        //checks if it has participant object pointed at it
        //removes active participant by replacing with new default one.
        //When you leave a field, overwrite its status so that you own it.
        public void UpdateLeavingArenaFieldPoint(Participant_DTO pDTO, string arena)
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
                        if (AFP_DTO.PDTO.TeamColorGS.Equals("Blue"))
                        {
                            //AFP_DTO.PDTO.PointGS = null;
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
            }//CHECK THIS LATER
        }

        //When you move to a field, overwrite it's status so that you occupy it
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


    }
}
