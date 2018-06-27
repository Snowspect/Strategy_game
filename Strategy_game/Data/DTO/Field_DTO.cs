using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DTO
{
    class Field_DTO
    {
        private Dictionary<Participant_DTO, FieldPoint_DTO> field;
        public Field_DTO()
        {
            FieldGS = new Dictionary<Participant_DTO, FieldPoint_DTO>();
        }

        //get / set (add or remove as well) from dictionary
        public Dictionary<Participant_DTO, FieldPoint_DTO> FieldGS { get => field; set => field = value; }
    }
}
