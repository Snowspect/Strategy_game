using Strategy_game.Data;
using Strategy_game.Data.DAO;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_Impl;
using Strategy_game.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Func
{
    /// <summary>
    /// Handles the implementation logic related to participants
    /// </summary>
    public class Fighter_Impl : IParticipant_IntImpl_Generic<string, int, Fighter_DTO, ArenaFieldPoint_DTO>
    {
        #region localVariables
        Participant_DAO pDAO;
        Arena_Impl ArenaImpl;
        Team_DAO team;
        int skinCounter = 0;
        #endregion

        #region constructor
        public Fighter_Impl()
        {
            this.pDAO = new Participant_DAO();
            this.team = new Team_DAO();
            this.ArenaImpl = new Arena_Impl();
        }
        
        public Fighter_Impl(Participant_DAO pDAO, Team_DAO team_DAO, Arena_Impl ArenaImpl)
        {
            this.pDAO = pDAO;
            team = team_DAO;
            this.ArenaImpl = ArenaImpl;
        }
        #endregion

        /*
         * GetParticipant
         * AddParticipantToList
         * UpdateFieldToParticipant
         * GetCurrentList
         * GetImageFromParticipant
         */
        #region methods
        /// <summary>
        /// gets a fighter based on a name
        /// </summary>
        /// <param name="participant_name"></param>
        /// <returns></returns>
        public Fighter_DTO GetParticipant(string participant_name)
        {
            Fighter_DTO pDTO = pDAO.GetParticipant_DTODB(participant_name);
            return pDTO;
        }

        /// <summary>
        /// Sets is coords of the field to move to
        /// Assigns the fighter a skin
        /// Finally adds the participant to the new field
        /// </summary>
        /// <param name="pDTO"></param>
        /// <param name="AFP_DTO"></param>
        public void MoveParticipant(Fighter_DTO pDTO, ArenaFieldPoint_DTO AFP_DTO)
        {
            pDTO.PointGS = AFP_DTO; //updates point reference
            AssignPicture(pDTO);
            ArenaImpl.AddParticipantToField(pDTO); //creates a reference binding between active participant and the field it is moving to
        }

        public void AssignPicture(Fighter_DTO pDTO)
        {
            if (pDTO.ImageGS.Equals("NoPicture"))
            {
                pDTO.ImageGS = pDAO.GetAllianceSkin(skinCounter);
                skinCounter++;
            }
        }
        //Adds a participant to storage
        public void AddParticipantToList(Fighter_DTO pDTO) /*adds it to static layer in storage class*/ { pDAO.AddToStorage(pDTO); }
        
        //adds a field to a participant
        public void UpdateFieldToParticipant(Fighter_DTO pDTO) {
            /*Access DTO in database, add field information*/
            pDAO.UpdateFieldToParticipant(pDTO); }

        //returns current Participant DTO
        public List<Fighter_DTO> GetCurrentList() => pDAO.GetParticipantList(); //gets from static layer in st

        //calls DAO to get image filepath from participant
        public string getImageFromParticipant(Fighter_DTO pDTO)
        {
            return pDTO.ImageGS;
            //return pDAO.GetParticipant_DTODB(pDTO.NameGS).ImageGS;
        }

        //gets fields within range and checks if player can move to them
        //returns true if so otherwize false
        public bool CheckMovement(Fighter_DTO pDTO, ArenaFieldPoint_DTO AFP_DTO)
        {
            List<ArenaFieldPoint_DTO> allowedMovementRange = GetMovementRange(pDTO);

            foreach (ArenaFieldPoint_DTO AFPDTO in allowedMovementRange)
            {
                if (AFPDTO.XPoint == AFP_DTO.XPoint && AFPDTO.YPoint == AFP_DTO.YPoint) //if given field coords match any coords within allowed range, then return true
                {
                    return true;
                }
            }
            MessageBoxResult resu = MessageBox.Show("this field is out of your range");
            return false;
        }

        //
        public List<ArenaFieldPoint_DTO> GetMovementRange(Fighter_DTO pDTO)
        {
            int allowedSteps = pDTO.MoveGS;
            int medioX = pDTO.PointGS.XPoint;
            int medioY = pDTO.PointGS.YPoint;
            List<ArenaFieldPoint_DTO> allowedMovementRange = new List<ArenaFieldPoint_DTO>();
            #region get fields from left and right
            for (int i = medioX + 1; i <= medioX + allowedSteps; i++) //goes from x coord + 1 to x coord + allowed steps
            {
                if (i <= 6) //makes sure we don't exceed the highest x coord
                {
                    allowedMovementRange.Add(ArenaImpl.GetArenaField(i, medioY)); //Adds number of "right" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            for (int i = medioX - 1; i >= medioX - allowedSteps; i--) //goes from x coord minus 1 to medioX-allowedsteps
            {
                if (i > 0) //makes sure we don't exceeed the lowest xcoord
                {
                    allowedMovementRange.Add(ArenaImpl.GetArenaField(i, medioY)); //Adds number of "left" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            #endregion
            #region get fields from top and bottom
            for (int i = medioY + 1; i <= medioY + allowedSteps; i++) //goes from y coord +1 to y coord + allowedsteps
            {
                if (i <= 6)
                {
                    allowedMovementRange.Add(ArenaImpl.GetArenaField(medioX, i)); //Adds number of "upper" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            for (int i = medioY - 1; i >= medioY - allowedSteps; i--) //goes from y coord - 1 to y coord - allowedSteps
            {
                if (i > 0)
                {
                    allowedMovementRange.Add(ArenaImpl.GetArenaField(medioX, i)); //Adds number of "lower" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            #endregion

            return allowedMovementRange;
        }

        //gets a list of possible ally members to attack, can return empty.
        public List<ArenaFieldPoint_DTO> CheckSurroundingFields(Fighter_DTO pDTO)
        {
            List<ArenaFieldPoint_DTO> allowedMovementRange = GetMovementRange(pDTO);
            List<ArenaFieldPoint_DTO> allyMembersToAttack = new List<ArenaFieldPoint_DTO>();
            foreach (ArenaFieldPoint_DTO AFPDTO in allowedMovementRange)
            {
                if(AFPDTO.PDTO != null)
                {
                    if (AFPDTO.PDTO.TeamColorGS.Equals("purple")) //check for alliance team
                    {
                        allyMembersToAttack.Add(AFPDTO); //Adds this field to 
                    }
                }
            }
            return allyMembersToAttack;
        }


        #endregion 
        public List<int> ParseInts(List<string> tp)
        {
            List<int> parsedInts = new List<int>();
            //Tests if one of the items are not an integer
            foreach (var item in tp)
            {
                try
                {
                    if (int.TryParse(item, out int result) == false) //result is not used, returns 0 since it fails.
                    {
                        throw new NotInteger("This is not a number: " + item);
                    }
                    else
                    {
                        parsedInts.Add(int.Parse(item));
                    }
                }
                catch (NotInteger b)
                {
                    Console.WriteLine(b);
                    Console.WriteLine(b.StackTrace.ToString());
                }
                catch (Exception exec)
                {
                    Console.WriteLine(exec.StackTrace.ToString());
                    MessageBox.Show("Exception not related to integerParsing");
                }
            }
            return parsedInts;
        }
        public string GetAllianceSkin(int counter)
        {
            return pDAO.GetAllianceSkin(counter);
        }
        public string GetHordeskin(int counter)
        {
            return pDAO.GetHordeSkin(counter);
        }
    }
}
