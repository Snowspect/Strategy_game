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
    class Field_Impl : IField_Impl<int, Participant_DTO, FieldPoint_DTO>
    {
        Field_DTO fDTO;
        FieldPoint_Impl fImpl;
        public Field_Impl()
        {
            fDTO = new Field_DTO();
            fImpl = new FieldPoint_Impl();
        }

        public void AddParticipantToField(Participant_DTO pDTO)
        {
            pDTO.PointGS = fImpl.GetArenaField(pDTO.PointGS.XPoint, pDTO.PointGS.YPoint); //Adds correct point object to participant
            pDTO.PointGS.PDTO = pDTO; //Adds participant to field
        }

        public void AddPointToField(FieldPoint_DTO fpDTO)
        {
            fDTO.FieldGS.Add(fpDTO);
        }


        public void EmptyField()
        {
            fDTO.FieldGS.Clear();
        }
    }
}
