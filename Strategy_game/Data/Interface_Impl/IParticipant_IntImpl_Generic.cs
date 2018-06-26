using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface IParticipant_IntImpl_Generic<T>
    {
        /// <summary>
        /// Replace T with desired parameter
        /// Use to access DAO in data layer (that then will add to storage
        /// </summary>
        void AddToList(T pDTO);
        /// <summary>
        /// Use to access DAO and data layer that will then retrieve from storage
        /// </summary>
        List<T> GetCurrentList();
    }
}
