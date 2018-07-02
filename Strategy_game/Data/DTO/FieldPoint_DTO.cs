using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DTO
{
    class FieldPoint_DTO
    {
        public int XPoint { get; set; }
        public int YPoint { get; set; }
        
        public FieldPoint_DTO()
        {
            XPoint = 0;
            YPoint = 0;
        }
        public FieldPoint_DTO(int x, int y)
        {
            YPoint = y;
            XPoint = x;
        }

        public String ToString => "XPoint: " + XPoint + ", YPoint: " + YPoint;
    }
}
