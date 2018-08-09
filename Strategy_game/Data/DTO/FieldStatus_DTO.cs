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
            AllianceOwned = 1,
            AllianceOccupied = 2,
            HordeOwned = 3,
            HordeOccupied = 4,
            notOccupied = 5
        }
    }
}
