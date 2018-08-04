using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    interface ICreateParticipantWindow_Impl<Z,E,P>
    {
        void ClearFields();
        List<E> ParseInts(List<Z> tp);
        void RetrieveInput(P pDTO, List<E> parsedTP);
    }
}
