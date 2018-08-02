using Strategy_game.Data.Interface_Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{
    class Field_Impl : IField_Impl<int>
    {
        public int ChangeFieldPointStatus()
        {
            return 0;
        }

        public int CheckFieldPoint()
        {
            throw new NotImplementedException();
        }

        public int GetParticipantOnFieldPoint()
        {
            throw new NotImplementedException();
        }
    }
}
