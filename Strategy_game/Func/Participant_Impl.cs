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
    /// <summary>
    /// Handles the implementation logic related to participants
    /// </summary>
    public class Member_Impl : IParticipant_IntImpl_Generic<Member_DTO, string>
    {
        #region localVariables
        Member_DAO pDAO;
        Member_DTO pDTO;
        Arena_Impl ArenaImpl;
        FieldPoint_Impl fPImpl;
        Team_DAO team;
        #endregion

        #region constructor
        public Member_Impl()
        {
            pDAO = new Member_DAO();
            pDTO = new Member_DTO();
            team = new Team_DAO();
            ArenaImpl = new Arena_Impl();
            fPImpl = new FieldPoint_Impl();
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
        //returns a participant based on a name
        public Member_DTO GetParticipant(string participant_name)
        {
            //Access DAO, return Participant.
            pDTO = pDAO.GetParticipant_DTODB(participant_name);
            return pDTO;
        }

        public void MoveParticipant(Member_DTO pDTO, ArenaFieldPoint_DTO AFP_DTO)
        {
            pDTO.PointGS = AFP_DTO; //updates point reference
            AssignPicture(pDTO);
            ArenaImpl.AddParticipantToField(pDTO); //creates a reference binding between active participant and the field it is moving to
        }

        public void AssignPicture(Member_DTO pDTO)
        {
            if (pDTO.ImageGS.Equals("NoPicture"))
            {
                pDTO.ImageGS = Storage.PlayerSkins[Storage.skinCounter];
                Storage.skinCounter++;
            }
        }
        //Adds a participant to storage
        public void AddParticipantToList(Member_DTO pDTO) /*adds it to static layer in storage class*/ { pDAO.AddToStorage(pDTO); }
        
        //adds a field to a participant
        public void UpdateFieldToParticipant(Member_DTO pDTO) {
            /*Access DTO in database, add field information*/
            pDAO.UpdateFieldToParticipant(pDTO); }

        //returns current Participant DTO
        public List<Member_DTO> GetCurrentList() => pDAO.GetParticipantList(); //gets from static layer in st

        //calls DAO to get image filepath from participant
        public string getImageFromParticipant(Member_DTO pDTO)
        {
            return pDTO.ImageGS;
            //return pDAO.GetParticipant_DTODB(pDTO.NameGS).ImageGS;
        }

        //gets fields within range and checks if player can move to them
        //returns true if so otherwize false
        public bool CheckMovement(Member_DTO pDTO, ArenaFieldPoint_DTO AFP_DTO)
        {
            List<ArenaFieldPoint_DTO> allowedMovementRange = GetMovementRange(pDTO);

            foreach (ArenaFieldPoint_DTO AFPDTO in allowedMovementRange)
            {
                if (AFPDTO.XPoint == AFP_DTO.XPoint && AFPDTO.YPoint == AFP_DTO.YPoint) //if given field coords match any coords within allowed range, then return true
                {
                    MessageBoxResult res = MessageBox.Show("You can move and will begin moving");
                    return true;
                }
            }
            MessageBoxResult resu = MessageBox.Show("this field is out of your range");
            return false;
        }

        //
        public List<ArenaFieldPoint_DTO> GetMovementRange(Member_DTO pDTO)
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
                    allowedMovementRange.Add(fPImpl.GetArenaField(i, medioY)); //Adds number of "right" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            for (int i = medioX - 1; i >= medioX - allowedSteps; i--) //goes from x coord minus 1 to medioX-allowedsteps
            {
                if (i > 0) //makes sure we don't exceeed the lowest xcoord
                {
                    allowedMovementRange.Add(fPImpl.GetArenaField(i, medioY)); //Adds number of "left" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            #endregion
            #region get fields from top and bottom
            for (int i = medioY + 1; i <= medioY + allowedSteps; i++) //goes from y coord +1 to y coord + allowedsteps
            {
                if (i <= 6)
                {
                    allowedMovementRange.Add(fPImpl.GetArenaField(medioX, i)); //Adds number of "upper" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            for (int i = medioY - 1; i >= medioY - allowedSteps; i--) //goes from y coord - 1 to y coord - allowedSteps
            {
                if (i > 0)
                {
                    allowedMovementRange.Add(fPImpl.GetArenaField(medioX, i)); //Adds number of "lower" allowedSteps AFP_DTOs to allowedMovementRange
                }
            }
            #endregion

            return allowedMovementRange;
        }

        //gets a list of possible ally members to attack, can return empty.
        public List<ArenaFieldPoint_DTO> CheckSurroundingFields(Member_DTO pDTO)
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


        #region subregion - wkA,stA,immA
        //Has the role of inserting into the participant and in that case also contacting database and changing participant information.
        //also returns participant directly to call so the participant is ready for battle directly (could even be with a buff that lets you change that attribute)
        public Member_DTO AddStrongAgainst(string pName_StAgainst, Member_DTO pDTO) { throw new NotImplementedException(); }
        public Member_DTO RemoveStrongAgainst(string pName_StAgainst, Member_DTO pDTO) { throw new NotImplementedException(); }
        public string GetStrongAgainst() { throw new NotImplementedException(); }

        public Member_DTO AddWeakAgainst(string pName_WkAgainst, Member_DTO pDTO) { throw new NotImplementedException(); }
        public Member_DTO RemoveWeakAgainst(string pName_WkAgainst, Member_DTO pDTO) { throw new NotImplementedException(); }
        public string GetWeakAgainast() { throw new NotImplementedException(); }

        public Member_DTO AddImmuneAgainst(string pNameImmAgainast, Member_DTO pDTO) { throw new NotImplementedException(); }
        public Member_DTO RemoveImmuneAgainst(string pNameImmAgainast, Member_DTO pDTO) { throw new NotImplementedException(); }
        public string GetImmuneAgainst() { throw new NotImplementedException(); }
        #endregion
        #endregion 
    }
}
