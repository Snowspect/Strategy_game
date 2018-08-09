using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface IParticipant_IntDAO_Generic<F,S, I>
    {
        void AddToStorage(F pDTO);
        List<F> GetParticipantList();
        void UpdateFieldToParticipant(F pDTO);
        F GetParticipant_DTODB(S participant_name);
        S GetAllianceSkin(I counter);
        S GetHordeSkin(I counter);
    }
}
