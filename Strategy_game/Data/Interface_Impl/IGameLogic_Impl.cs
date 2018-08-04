using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{ 
    interface IGameLogic_Impl<P,FP,I,S>
    {
        void AddParticipantToField(P pDTO);
        List<FP> GetField();
        void MoveParticipant(P pDTO);
        void EmptyField();
        void AddPointToField(FP fpDTO);
    }
}
