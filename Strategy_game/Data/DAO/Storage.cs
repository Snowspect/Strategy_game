using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DAO
{
    class Storage
    {
        public static List<Participant_DTO> StParticipant = new List<Participant_DTO>();

        public void AddToLayer(Participant_DTO pDTO)
        {
                StParticipant.Add(pDTO);
        }
        public List<Participant_DTO> GetParticipantList()
        {
            return StParticipant;
        }
    }
}
