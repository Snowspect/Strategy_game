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
    public class ArenaFieldPoint_DTO : System.Attribute
    {
        #region localVariables
        Participant_DTO pDTO = null;
        private FieldStatus_DTO.FieldStatus FieldPointStatus = FieldStatus_DTO.FieldStatus.notOccupied;
        #endregion

        #region constructor
        public ArenaFieldPoint_DTO()
        {
            XPoint = 0;
            YPoint = 0;
        }
        public ArenaFieldPoint_DTO(int x, int y)
        {
            YPoint = y;
            XPoint = x;
        }
        #endregion

        #region propertyAccess
        public int XPoint { get; set; }
        public int YPoint { get; set; }

        
        public FieldStatus_DTO.FieldStatus FieldPointStatusGS { get => FieldPointStatus; set => FieldPointStatus = value; }
        public Participant_DTO PDTO { get => pDTO; set => pDTO = value; }

        public override string ToString()
        {
            return "x" + XPoint + "y" + YPoint;
        }
        #endregion
    }
}
