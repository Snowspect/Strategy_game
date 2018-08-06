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
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.enemyOwned;
                            MessageBoxResult res = MessageBox.Show("The enemy now owns this field");
                        }
                        else if (AFP_DTO.PDTO.TeamColorGS.Equals("purple"))
                        {
                            AFP_DTO.PDTO = null;
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.selfOwned;
                            MessageBoxResult res = MessageBox.Show("You now owns this field");
                        }
                    }
                }
                else //So we don't occupy fields in the preArena window, but simply move our players around
                {
                    ArenaFieldPoint_DTO AFP_DTO = GetArenaField(xComingFrom, yComingFrom);
                    if (AFP_DTO.PDTO != null)
                    {
                        AFP_DTO.PDTO = null;

                        if (AFP_DTO.FieldPointStatusGS == FieldStatus_DTO.FieldStatus.selfOccupied)
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.selfOwned;
                        else
                            AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.enemyOwned;
                    }
                }
            }//CHECK THIS LATER
        }

        //When you move to a field, overwrite it's status so that you occupy it
        public void UpdateMovingToArenaFieldStatus(int xMovingTo, int yMovingTo, string arena)
        {
            ArenaFieldPoint_DTO AFP_DTO = GetArenaField(xMovingTo, yMovingTo);
            if (AFP_DTO.PDTO != null)
            {
                if (AFP_DTO.PDTO.TeamColorGS.Equals("Blue"))
                {
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.enemyOccupied;
                    MessageBoxResult res = MessageBox.Show("The enemy is now on this field");
                }
                else if (AFP_DTO.PDTO.TeamColorGS.Equals("purple"))
                {
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.selfOccupied;
                    if (!arena.Equals("preArena"))
                    {
                        MessageBoxResult res = MessageBox.Show("You are now on this field");
                    }
                }
            }
        }

        //Checks for what is on the field at this given time.
        public bool CheckField(int x, int y, Participant_DTO pDTO)
        {
            foreach (ArenaFieldPoint_DTO AFP_DTO in Arena_DTO.field)
            {
                if (AFP_DTO.XPoint == x && AFP_DTO.YPoint == y)
                {
                    if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.notOccupied))
                    {
                        MessageBoxResult res = MessageBox.Show("You have moved");
                        //The player can move here
                        return true;
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.selfOccupied))
                    {
                        if(pDTO.TeamColorGS.Equals("purple"))
                        {
                            MessageBoxResult res = MessageBox.Show("An ally is already here");
                            return false;
                        }
                        else
                        {
                            MessageBoxResult res = MessageBox.Show("The enemy destroyed your player");
                            return true;
                        }
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.selfOwned))
                    {
                        MessageBoxResult res = MessageBox.Show("Moved to purple team owned field");
                        return true;
                        //Just move to the coords
                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.enemyOccupied))
                    {
                        //in case either ally or enemy attempts to move to that spot
                        if(pDTO.TeamColorGS.Equals("purple"))
                        {

                            MessageBoxResult result = MessageBox.Show("You destroyed the enemy");
                            return true;
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("An enemy ally is here");
                            return false;
                        }
                        //just move there

                    }
                    else if (AFP_DTO.FieldPointStatusGS.Equals(FieldStatus_DTO.FieldStatus.enemyOwned))
                    {
                        MessageBoxResult res = MessageBox.Show("You are getting a debuff by taking blue team spot, but only for 1 turn.. Not yet impl");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
