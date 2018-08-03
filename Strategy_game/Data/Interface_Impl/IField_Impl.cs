namespace Strategy_game.Data.Interface_Impl
{
    interface IField_Impl<T>
    {
        T CheckFieldPoint();
        T ChangeFieldPointStatus();
        T GetParticipantOnFieldPoint();
    }
}
