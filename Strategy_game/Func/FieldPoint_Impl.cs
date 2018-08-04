using Strategy_game.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{
    class FieldPoint_Impl
    {
        Field_DTO fDTO;
        public FieldPoint_Impl()
        {
            fDTO = new Field_DTO();
        }
        public FieldPoint_DTO GetArenaField(int x, int y)
        {
            foreach (FieldPoint_DTO fpDTO in fDTO.FieldGS)
            {
                if(fpDTO.XPoint == x && fpDTO.YPoint == y)
                {
                    return fpDTO;
                }
            }
            return null;
        }
    }
}
