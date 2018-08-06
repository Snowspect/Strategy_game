namespace Strategy_game.Data.Interface_Impl
{
    interface IArena_Impl<T, P, FP>
    {
        void AddParticipantToField(P pDTO);
        void AddPointToField(FP fpDTO);
        void EmptyField();
    }
}
