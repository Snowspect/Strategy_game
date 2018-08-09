using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_windows
{
    interface ICreateParticipantWindow_Impl<I,F>
    {
        void RetrieveInput(F pDTO, List<I> parsedTP);
        void ClearFields();
    }
}
