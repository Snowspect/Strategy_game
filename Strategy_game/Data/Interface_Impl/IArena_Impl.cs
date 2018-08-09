using System.Collections.Generic;

namespace Strategy_game.Data.Interface_Impl
{
    interface IArena_Impl<S, I, F, AF, B>
    {
        void AddParticipantToField(F pDTO);

        void AddFieldToArena(AF fpDTO);

        void CreateFullArena();

        void DeleteFighterFromArena(F pDTO);

        void EmptyField();

        List<AF> GetField();

        B CheckField(AF AFP_DTO_param, F pDTO);

        AF GetArenaField(int x, int y);

        void UpdateLeavingArenaFieldPoint(F pDTO, S arena);

        void UpdateMovingToArenaFieldStatus(AF AFP_DTO, S arena);

        List<F> GetActivePlayers();

        void SetActivePlayers(List<F> ActivePlayers);

    }
}
