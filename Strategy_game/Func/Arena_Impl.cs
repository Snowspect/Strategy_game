using Strategy_game.Data;
using Strategy_game.Data.DTO;
using Strategy_game.Data.Interface_Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Strategy_game.Func
{
    public class Arena_Impl : IArena_Impl<int, Member_DTO, ArenaFieldPoint_DTO>
    {
        
        ArenaFieldPoint_Impl fImpl;
        public Arena_Impl()
        {
            fImpl = new ArenaFieldPoint_Impl();
        }

        public void AddParticipantToField(Member_DTO pDTO)
        {
            pDTO.PointGS = fImpl.GetArenaField(pDTO.PointGS.XPoint, pDTO.PointGS.YPoint); //Adds correct point object to participant
            pDTO.PointGS.PDTO = pDTO; //Adds participant to Arena
        }

        public void AddPointToField(ArenaFieldPoint_DTO fpDTO)
        {
            Arena_DTO.field.Add(fpDTO);
        }

        public void CreateFullArena() //Burde tag et argument af AFP_DTO så der ikke er tilknytning mellem den her og ArenaFieldPoint_Impl
        {
            int height = 6;
            int length = 6;

            for (int y = height; y > 0; y--) //x is 6, decreased to 1
            {
                for (int x = 1; x < length + 1; x++) // y is 1, increased to 6 
                {
                    ArenaFieldPoint_DTO AFP_DTO = new ArenaFieldPoint_DTO();
                    AFP_DTO.XPoint = x;
                    AFP_DTO.YPoint = y;
                    AFP_DTO.FieldPointStatusGS = FieldStatus_DTO.FieldStatus.notOccupied;
                    AddPointToField(AFP_DTO);
                }
            }
            Arena_Impl tmp1 = new Arena_Impl();
            tmp1.GetField();
        }



        public void EmptyField()
        {
            Arena_DTO.field.Clear();
        }
        public List<ArenaFieldPoint_DTO> GetField()
        {
            return Arena_DTO.field;
        }
    }
}
