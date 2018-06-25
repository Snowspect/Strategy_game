using Strategy_game.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{  
    class Participant_DAO
    {

        Storage st = new Storage();
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
