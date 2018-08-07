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
    class Member_DAO : IParticipant_IntDAO_Generic<Member_DTO, ArenaFieldPoint_DTO, string>
    {
        Member_DTO pDTO;
        Storage st = new Storage();

        public Member_DAO()
        {
            pDTO = new Member_DTO();
        }

        //Adds participant to layer (currently only one storage class)
        public void AddToStorage(Member_DTO pDTO)
        {            
            st.AddToLayer(pDTO);
        }
        //gets lists of participants (currently just accessing storage class)
        public List<Member_DTO> GetParticipantList()
        {
            return st.GetParticipantList();
        }
        //Adds field to a specific particpants (currently just accessing storage class
        public void UpdateFieldToParticipant(Member_DTO pDTO)
        {
            st.UpdateFieldToParticipant(pDTO, pDTO.PointGS);
        }
        //Gets participant from storage (ment to do database retrieval)
        public Member_DTO GetParticipant_DTODB(string participant_name)
        {
            pDTO = st.GetParticipant_DTOST(participant_name); //placeholder for database access
            return pDTO;
        }
    }
}