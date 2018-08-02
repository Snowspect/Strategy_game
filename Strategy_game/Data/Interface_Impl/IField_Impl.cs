using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface IField_Impl<T>
    {
        T CheckFieldPoint();
        T ChangeFieldPointStatus();
        T GetParticipantOnFieldPoint();
    }
}
