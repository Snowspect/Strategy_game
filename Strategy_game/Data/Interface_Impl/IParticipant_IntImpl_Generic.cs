using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_Impl
{
    interface IParticipant_IntImpl_Generic<S,I,F,AF>
    {
        F GetParticipant(S participant_name);

        void MoveParticipant(F pDTO, AF AFP_DTO);

        void AssignPicture(F pDTO);

        void AddParticipantToList(F pDTO);

        void UpdateFieldToParticipant(F pDTO);

        List<F> GetCurrentList();

        S getImageFromParticipant(F pDTO);

        bool CheckMovement(F pDTO, AF AFP_DTO);

        List<AF> GetMovementRange(F pDTO);

        List<AF> CheckSurroundingFields(F pDTO);

        List<I> ParseInts(List<S> tp);

    }
}
