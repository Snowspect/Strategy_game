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
        //private static List<Participant_DTO> participantsInField;

        public static List<Fighter_DTO> AllianceTeam = new List<Fighter_DTO>();
        public static List<Fighter_DTO> HordeTeam = new List<Fighter_DTO>();
        public static List<Fighter_DTO> ActiveFighters = new List<Fighter_DTO>();

        public Arena_DTO()
        {
        }

        
        //get / set (add or remove as well) from dictionary
        public List<ArenaFieldPoint_DTO> FieldGS { get => field; set => field = value; }
    }
}
