using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface IParticipant_IntDAO_Generic<T,E,S>
    {
        /// Replace T with desired parameter
        /// Use to add to Storage
        void AddToStorage (T DTO);

        /// Replace T with desired parameter
        /// Use to retrieve from storage
        List<T> GetParticipantList();

        //Adds a fieldPoint_DTO to a participant dto.
        void AddFieldToParticipant(T pDTO, E fpDTO);

        //returns a participant from storage
        Participant_DTO GetParticipant_DTODB(S participant_name);
    }
}
