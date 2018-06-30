using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy_game.Data.DTO;
using Strategy_game.Data;

namespace Strategy_game.Func
{
    class Game_Logic_Impl
    {
        //Dictionary<Participant_DTO, FieldPoint_DTO> field = new Dictionary<Participant_DTO, FieldPoint_DTO>();
        //List<FieldPoint_DTO> fields;
        Field_DTO field;

        public Game_Logic_Impl()
        {
            field = new Field_DTO();
        }

        // Adds a participant to the field
        public void AddParticipantToField(Participant_DTO pDTO)
        {
            field.FieldGS.Add(Tuple.Create(pDTO,pDTO.PointGS)); //using tuple due to dictionary only containing unique sets.
        }

        //returns field
        public List<Tuple<Participant_DTO, FieldPoint_DTO>> GetField()
        {
            return field.FieldGS;
        }
    }
}
