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
    public class Participant_DAO : IParticipant_IntDAO_Generic<Fighter_DTO, string, int>
    {
        Fighter_DTO pDTO;
        Storage st = new Storage();

        public Participant_DAO()
        {
            pDTO = new Fighter_DTO();
        }

        //Adds participant to layer (currently only one storage class)
        public void AddToStorage(Fighter_DTO pDTO)
        {
            st.AddToLayer(pDTO);
        }
        //gets lists of participants (currently just accessing storage class)
        public List<Fighter_DTO> GetParticipantList()
        {
            return st.GetParticipantList();
        }
        //Adds field to a specific particpants (currently just accessing storage class
        public void UpdateFieldToParticipant(Fighter_DTO pDTO)
        {
            st.UpdateFieldToParticipant(pDTO, pDTO.PointGS);
        }
        //Gets participant from storage (ment to do database retrieval)
        public Fighter_DTO GetParticipant_DTODB(string participant_name)
        {
            pDTO = st.GetParticipant_DTOST(participant_name); //placeholder for database access
            return pDTO;
        }
        public string GetAllianceSkin(int counter)
        {
            return Storage.AllianceSkins[counter];
        }
        public string GetHordeSkin(int counter)
        {
            return Storage.HordeSkins[counter];
        }

    }
}