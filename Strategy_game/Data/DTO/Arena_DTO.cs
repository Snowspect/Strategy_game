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
    public class Arena_DTO
    {
        public static List<ArenaFieldPoint_DTO> field = new List<ArenaFieldPoint_DTO>();
        private static List<Participant_DTO> participantsInField;

        public Arena_DTO()
        {
//            FieldGS = new List<ArenaFieldPoint_DTO>();
        }

        #region property accessers
        public static List<Participant_DTO> ParticipantsInField { get => participantsInField; set => participantsInField = value; }

        //get / set (add or remove as well) from dictionary
        public List<ArenaFieldPoint_DTO> FieldGS { get => field; set => field = value; }
        #endregion
    }
}
