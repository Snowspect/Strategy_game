using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DTO
{
    /// <summary>
    /// is to hold account for a current battle
    /// </summary>
    public class Field_DTO
    {
        private static List<FieldPoint_DTO> field;
        private static List<Participant_DTO> participantsInField;

        public Field_DTO()
        {
            FieldGS = new List<FieldPoint_DTO>();
        }

        #region property accessers
        public static List<Participant_DTO> ParticipantsInField { get => participantsInField; set => participantsInField = value; }

        //get / set (add or remove as well) from dictionary
        public List<FieldPoint_DTO> FieldGS { get => field; set => field = value; }
        #endregion
    }
}
