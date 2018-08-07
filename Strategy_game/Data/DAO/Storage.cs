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
        public static int skinCounter = 0;
        public static List<Participant_DTO> StParticipant = new List<Participant_DTO>();
        public static Dictionary<string, string> teams = new Dictionary<string, string>();
        private static List<string> playerSkins = new List<string>();

        public static List<string> PlayerSkins { get => playerSkins; set => playerSkins = value; }

        //adds a participant to the storage /participant represnet one fighter on the field"
        public void AddToLayer(Participant_DTO pDTO)
        {
                StParticipant.Add(pDTO);
        }
        //Gets lists of participants
        public List<Participant_DTO> GetParticipantList()
        {
            return StParticipant;
        }
        //Adds field to a specific participant in storage
        public void AddFieldToParticipant(Participant_DTO pDTO, ArenaFieldPoint_DTO fpDTO)
        {
           
        }

        internal void UpdateFieldToParticipant(Participant_DTO pDTO, ArenaFieldPoint_DTO fpDTO)
        {
            foreach (var element in StParticipant) if (element.NameGS.Equals(pDTO.NameGS)) element.PointGS = fpDTO;
        }

        public Participant_DTO GetParticipant_DTOST(string participant_name) //ST = storage
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
