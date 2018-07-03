using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DTO
{
    public class Field_DTO
    {
        private List<Tuple<Participant_DTO, FieldPoint_DTO>> field;
        public Field_DTO()
        {
            FieldGS = new List<Tuple<Participant_DTO, FieldPoint_DTO>>();
        }

        //get / set (add or remove as well) from dictionary
        public List<Tuple<Participant_DTO, FieldPoint_DTO>> FieldGS { get => field; set => field = value; }
    }
}
