using Strategy_game.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Temporary class (will be deleted once DB is implemented in Participant_DAO
/// </summary>
namespace Strategy_game.Data.DAO
{
    class Storage
    {
        public static List<Fighter_DTO> StParticipant = new List<Fighter_DTO>();
        public static Dictionary<string, string> teams = new Dictionary<string, string>();
        //private static List<string> playerSkins = new List<string>();
        public static List<string> AllianceSkins = new List<string>();
        public static List<string> HordeSkins = new List<string>();
        public static Dictionary<string, string> teamList = new Dictionary<string, string>();
        public static Dictionary<string, string> enemyTeamList = new Dictionary<string, string>();



        //  public static List<string> PlayerSkins { get => playerSkins; set => playerSkins = value; }

        //adds a participant to the storage /participant represnet one fighter on the field"
        public void AddToLayer(Fighter_DTO pDTO)
        {
                StParticipant.Add(pDTO);
        }
        //Gets lists of participants
        public List<Fighter_DTO> GetParticipantList()
        {
            return StParticipant;
        }
        //Adds field to a specific participant in storage
        public void AddFieldToParticipant(Fighter_DTO pDTO, ArenaFieldPoint_DTO fpDTO)
        {
           
        }

        internal void UpdateFieldToParticipant(Fighter_DTO pDTO, ArenaFieldPoint_DTO fpDTO)
        {
            foreach (var element in StParticipant) if (element.NameGS.Equals(pDTO.NameGS)) element.PointGS = fpDTO;
        }

        public Fighter_DTO GetParticipant_DTOST(string participant_name) //ST = storage
        {
            foreach (var item in StParticipant)
            {
                if (item.NameGS.Equals(participant_name))
                    return item;
            }
            return null;
        }
    }
}
