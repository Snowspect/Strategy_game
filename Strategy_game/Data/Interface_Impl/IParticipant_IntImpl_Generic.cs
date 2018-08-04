using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface IParticipant_IntImpl_Generic<T,E,Z>
    {
        /// <summary>
        /// Replace T with desired parameter
        /// Use to access DAO in data layer (that then will add to storage
        /// </summary>
        void AddParticipantToList(T pDTO);
        /// <summary>
        /// Use to access DAO and data layer that will then retrieve from storage
        /// </summary>
        List<T> GetCurrentList();

        void UpdateFieldToParticipant(E fpDTO, T pDTO);

        T GetParticipant(Z participant_name);

        string getImageFromParticipant(Z participant_name);

        /// Replace T with desired parameter
        /// THe intended is a string
        /// Getters and setters for enemies
        T AddStrongAgainst(Z participant_name, T pDTO);
        T RemoveStrongAgainst(Z participant_name, T pDTO);
        Z GetStrongAgainst();
        T AddWeakAgainst(Z participant_name, T pDTO);
        T RemoveWeakAgainst(Z participant_name, T pDTO);
        Z GetWeakAgainast();
        T AddImmuneAgainst(Z participant_name , T pDTO);
        T RemoveImmuneAgainst(Z participant_name , T pDTO);
        Z GetImmuneAgainst();
    }
}
