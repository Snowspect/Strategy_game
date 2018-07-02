using Strategy_game.Data.DAO;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{  
    class Participant_DAO : IParticipant_IntDAO_Generic<Participant_DTO>
    {
        Participant_DTO pDTO;
        Storage st = new Storage();

        public Participant_DAO()
        {
            pDTO = new Participant_DTO();
        }

        //Adds participant to layer (currently only one storage class)
        public void AddToLayer(Participant_DTO pDTO)
        {            
            st.AddToLayer(pDTO);
        }
        //gets lists of participants (currently just accessing storage class)
        public List<Participant_DTO> GetParticipantList()
        {
            return st.GetParticipantList();
        }
        //Adds field to a specific particpants (currently just accessing storage class
        public void AddFieldToParticipant(Participant_DTO pDTO, FieldPoint_DTO fpDTO)
        {
            st.AddFieldToParticipant(pDTO, fpDTO);
        }
        //Gets participant from storage (ment to do database retrieval)
        public Participant_DTO GetParticipant_DTODB(string Participant_name)
        {
            pDTO = st.GetParticipant_DTOST(); //placeholder for database access
            return pDTO;
        }
    }
}