using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface IParticipant_IntDAO_Generic<P,E,S>
    {
        /// Replace T with desired parameter
        /// Use to add to Storage
        void AddToStorage (P pDTO);

        /// Replace T with desired parameter
        /// Use to retrieve from storage
        List<P> GetParticipantList();

        //Adds a fieldPoint_DTO to a participant dto.
        void UpdateFieldToParticipant(P pDTO);

        //returns a participant from storage
        Member_DTO GetParticipant_DTODB(S participant_name);
    }
}
