using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{ 
    interface IGameLogic_Impl<T,E,S,Z>
    {
        void AddParticipantToField(T pDTO);
        List<Tuple<T, E>> GetField();
        void MoveParticipant(S xCoord, S yCoord, Z Participant_name);
        string GetImage(Z participant_name);
        string GetParticipantFieldCoord(Z participant_name);
        void AddTeam(Z teamName, Z imageName);
    }
}
