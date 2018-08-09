using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.Interface_DAO
{
    interface IArena_DAO<F,AF>
    {
        List<AF> GetField();
        void EmptyField();
        void RemoveAlliance(F pDTO);
        void RemoveHorde(F pDTO);
        void AddFieldToArena(AF fpDTO);
        List<F> GetActivePlayers();
        void SetActivePlayers(List<F> ActivePlayers);

    }
}
