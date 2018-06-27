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

    class Participant_Impl : IParticipant_IntImpl_Generic<Participant_DTO>
    {
        Participant_DAO pDAO;
        public Participant_Impl()
        {
            pDAO = new Participant_DAO();
        }

        //Adds a participant to storage
        public void AddToList(Participant_DTO pDTO) //adds it to static layer in storage class
        {
            pDAO.AddToLayer(pDTO);
        }

        //adds a field to a participant
        public void AddFieldToParticipant(FieldPoint_DTO fpDTO, Participant_DTO pDTO)
        {
            //Access DTO in database, add field information
            pDAO.AddFieldToParticipant(pDTO, fpDTO);
        }

        //returns current Participant DTO
        public List<Participant_DTO> GetCurrentList() => pDAO.GetParticipantList(); //gets from static layer in st
    }
}
