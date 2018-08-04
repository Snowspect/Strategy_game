using Strategy_game.Data;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{
    public class Arena_Impl : IArena_Impl<int, Participant_DTO, ArenaFieldPoint_DTO>
    {
        Arena_DTO fDTO;
        FieldPoint_Impl fImpl;
        public Arena_Impl()
        {
            fDTO = new Arena_DTO();
            fImpl = new FieldPoint_Impl();
        }

        public void AddParticipantToField(Participant_DTO pDTO)
        {
            pDTO.PointGS = fImpl.GetArenaField(pDTO.PointGS.XPoint, pDTO.PointGS.YPoint); //Adds correct point object to participant
            pDTO.PointGS.PDTO = pDTO; //Adds participant to Arena
        }

        public void AddPointToField(ArenaFieldPoint_DTO fpDTO)
        {
            fDTO.FieldGS.Add(fpDTO);
        }


        public void EmptyField()
        {
            fDTO.FieldGS.Clear();
        }
        public List<ArenaFieldPoint_DTO> GetField()
        {
            return fDTO.FieldGS;
        }
    }
}
