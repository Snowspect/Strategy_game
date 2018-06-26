using Strategy_game.Data.DAO;
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
        Storage st = new Storage();

        public Participant_DAO()
        {
        }

        public void AddToLayer(Participant_DTO pDTO)
        {            
            st.AddToLayer(pDTO);
        }
        public List<Participant_DTO> GetParticipantList()
        {
            return st.GetParticipantList();
        }
    }
}
