using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DTO
{
    /// <summary>
    /// Holds information regarding one individual point.
    /// </summary>
    public class FieldPoint_DTO : System.Attribute
    {
        #region localVariables
        public int XPoint { get; set; }
        public int YPoint { get; set; }
        private string fieldPointStatus = FieldStatus.notOccupied.ToString();
        public enum FieldStatus
        {
            selfOwned = 1,
            selfOccupied = 2,
            enemyOwned = 3,
            enemyOccupied = 4,
            notOccupied = 5
        }
        #endregion

        #region propertyAccess
        public FieldPoint_DTO()
        {
            XPoint = 0;
            YPoint = 0;
        }
        public FieldPoint_DTO(int x, int y)
        {
            YPoint = y;
            XPoint = x;
        }
        
        public new String ToString => "XPoint: " + XPoint + ", YPoint: " + YPoint;

        public string FieldPointStatus { get => fieldPointStatus; set => fieldPointStatus = value; }
        #endregion

    }
}
