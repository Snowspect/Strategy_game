using Strategy_game.Data;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{
    /// <summary>
    /// Handles the implementation logic related to participants
    /// </summary>
    class Participant_Impl : IParticipant_IntImpl_Generic<Participant_DTO, FieldPoint_DTO, string> //generic
    {
        Participant_DAO pDAO;
        Participant_DTO pDTO;
        public Participant_Impl()
        {
            pDAO = new Participant_DAO();
            pDTO = new Participant_DTO();
        }
        //returns a participant based on a name
        public Participant_DTO GetParticipant(string participant_name)
        {
            //Access DAO, return Participant.
            pDTO = pDAO.GetParticipant_DTODB(participant_name);
            return pDTO;
        }

        //Adds a participant to storage
        public void AddToList(Participant_DTO pDTO) /*adds it to static layer in storage class*/ { pDAO.AddToStorage(pDTO); }
        
        //adds a field to a participant
        public void UpdateFieldToParticipant(FieldPoint_DTO fpDTO, Participant_DTO pDTO) {
            /*Access DTO in database, add field information*/
            pDAO.AddFieldToParticipant(pDTO, fpDTO); }

        //returns current Participant DTO
        public List<Participant_DTO> GetCurrentList() => pDAO.GetParticipantList(); //gets from static layer in st

        //calls DAO to get image filepath from participant
        public string getImageFromParticipant(string participant_name)
        {
            return pDAO.GetParticipant_DTODB(participant_name).ImageGS;
        }

        //Has the role of inserting into the participant and in that case also contacting database and changing participant information.
        //also returns participant directly to call so the participant is ready for battle directly (could even be with a buff that lets you change that attribute)
        public Participant_DTO AddStrongAgainst(string pName_StAgainst, Participant_DTO pDTO) { throw new NotImplementedException(); }
        public Participant_DTO RemoveStrongAgainst(string pName_StAgainst, Participant_DTO pDTO) { throw new NotImplementedException(); }
        public string GetStrongAgainst() { throw new NotImplementedException(); }

        public Participant_DTO AddWeakAgainst(string pName_WkAgainst, Participant_DTO pDTO) { throw new NotImplementedException(); }
        public Participant_DTO RemoveWeakAgainst(string pName_WkAgainst, Participant_DTO pDTO) { throw new NotImplementedException(); }
        public string GetWeakAgainast() { throw new NotImplementedException(); }

        public Participant_DTO AddImmuneAgainst(string pNameImmAgainast, Participant_DTO pDTO) { throw new NotImplementedException(); }
        public Participant_DTO RemoveImmuneAgainst(string pNameImmAgainast, Participant_DTO pDTO) { throw new NotImplementedException(); }
        public string GetImmuneAgainst() { throw new NotImplementedException(); }

        public void AddTeam(string teamName, string imageName)
        {

        }
    }
}
