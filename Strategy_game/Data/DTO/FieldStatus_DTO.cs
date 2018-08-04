using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DTO
{
    public class FieldStatus_DTO
    {
        public enum FieldStatus
        {
            selfOwned = 1,
            selfOccupied = 2,
            enemyOwned = 3,
            enemyOccupied = 4,
            notOccupied = 5
        }
    }
}
