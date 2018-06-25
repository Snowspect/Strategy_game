using Strategy_game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{

    class Participant_Impl
    {
        Participant_DAO pDAO;
        public Participant_Impl()
        {
            pDAO = new Participant_DAO();
        }

        //returns current Participant DTO

        public void AddToList(Participant_DTO pDTO) => pDAO.AddToLayer(pDTO); //adds it to static layer in storage class

        public List<Participant_DTO> GetCurrentList() => pDAO.GetParticipantList(); //gets from static layer in st
    }
}
