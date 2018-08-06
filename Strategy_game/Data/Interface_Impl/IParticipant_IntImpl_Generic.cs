using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface IParticipant_IntImpl_Generic<P,S>
    {
        /// <summary>
        /// Replace T with desired parameter
        /// Use to access DAO in data layer (that then will add to storage
        /// </summary>
        void AddParticipantToList(P pDTO);
        /// <summary>
        /// Use to access DAO and data layer that will then retrieve from storage
        /// </summary>
        List<P> GetCurrentList();

        void UpdateFieldToParticipant(P pDTO);

        P GetParticipant(S participant_name);

        /// Replace T with desired parameter
        /// THe intended is a string
        /// Getters and setters for enemies
        P AddStrongAgainst(S participant_name, P pDTO);
        P RemoveStrongAgainst(S participant_name, P pDTO);
        S GetStrongAgainst();
        P AddWeakAgainst(S participant_name, P pDTO);
        P RemoveWeakAgainst(S participant_name, P pDTO);
        S GetWeakAgainast();
        P AddImmuneAgainst(S participant_name , P pDTO);
        P RemoveImmuneAgainst(S participant_name , P pDTO);
        S GetImmuneAgainst();
    }
}
