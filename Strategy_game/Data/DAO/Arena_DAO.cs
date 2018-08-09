using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DAO
{
    public class Arena_DAO : IArena_DAO<Fighter_DTO, ArenaFieldPoint_DTO>
    {
        Arena_DTO A_DTO;

        public Arena_DAO()
        {
            A_DTO = new Arena_DTO();
        }

        public Arena_DAO(Arena_DTO A_DTO)
        {
            this.A_DTO = A_DTO;
        }

        public List<ArenaFieldPoint_DTO> GetField()
        {
            return A_DTO.FieldGS;
        }
        public void EmptyField()
        {
            Arena_DTO.field.Clear();
        }
        public void RemoveAlliance(Fighter_DTO pDTO)
        {
            Arena_DTO.AllianceTeam.Remove(pDTO);
            Arena_DTO.ActiveFighters.Remove(pDTO);
        }
        public void RemoveHorde(Fighter_DTO pDTO)
        {
            Arena_DTO.HordeTeam.Remove(pDTO);
            Arena_DTO.ActiveFighters.Remove(pDTO);
        }
        public void AddFieldToArena(ArenaFieldPoint_DTO fpDTO)
        {
            Arena_DTO.field.Add(fpDTO);
        }
        public List<Fighter_DTO> GetActivePlayers()
        {
            return Arena_DTO.ActiveFighters;
        }
        public void SetActivePlayers(List<Fighter_DTO> ActivePlayers)
        {
            Arena_DTO.ActiveFighters = ActivePlayers;
        }
    }
}
