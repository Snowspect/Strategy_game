using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface IParticipant_IntImpl_Generic<T,E>
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

        /// Replace T with desired parameter
        /// THe intended is a string
        /// Getters and setters for enemies
        T AddStrongAgainst(E participant_name, T pDTO);
        T RemoveStrongAgainst(E participant_name, T pDTO);
        E GetStrongAgainst();
        T AddWeakAgainst(E participant_name, T pDTO);
        T RemoveWeakAgainst(E participant_name, T pDTO);
        E GetWeakAgainast();
        T AddImmuneAgainst(E participant_name , T pDTO);
        T RemoveImmuneAgainst(E participant_name , T pDTO);
        E GetImmuneAgainst();
    }
}
