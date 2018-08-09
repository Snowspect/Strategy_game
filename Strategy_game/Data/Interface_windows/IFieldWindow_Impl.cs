using System.Collections.Generic;

namespace Strategy_game.Data.Interface_windows
{
    interface IFieldWindow_Impl<F,AF>
    {
        void InitiateMovement(AF AFP_DTO);
        void ActivateMovement(F pDTO, AF AFP_DTO);
        void ClearsImage(F pDTO);
        void SetsImage(F pDTO);
        void CreateArena();
        void InsertParticipantsToField();
        void CheckNextParticipant();
        void UpdateList();
        List<F> GetActivePlayers();
        void ActivateAllianceTurn();
        void ActivateHordeTurn(F currentFighter);
        List<F> Shuffle(List<F> turnBasedMovementList);
    }
}
