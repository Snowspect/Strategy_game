using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface IParticipant_IntDAO_Generic<T>
    {
        /// Replace T with desired parameter
        /// Use to add to Storage
        void AddToLayer (T DTO);

        /// Replace T with desired parameter
        /// Use to retrieve from storage
        List<T> GetParticipantList();
    }
}
