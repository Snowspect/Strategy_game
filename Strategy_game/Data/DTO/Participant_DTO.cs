using Strategy_game.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    class Participant_DTO
    {
        public Participant_DTO()
        {
            HealthGS = 1;
            DefenceGS = 1;
            OffenceGS = 1;
            NameGS = "test";
            VMoveGS = 1;
            HMoveGS = 1;
            PointGS = new FieldPoint_DTO();
            PointGS.XPoint = 0;
            PointGS.YPoint = 0;

        }
        public Participant_DTO(int h, int d, int o, int Vm, int Hm, String n)
        {
            HealthGS = h;
            DefenceGS = d;
            OffenceGS = o;
            NameGS = n;
            VMoveGS = Vm;
            HMoveGS = Hm;
            PointGS = new FieldPoint_DTO();
            PointGS.XPoint = 0;
            PointGS.YPoint = 0;
        }

        public int VMoveGS { get; set; }
        public string NameGS { get; set; }
        public int OffenceGS { get; set; }
        public int DefenceGS { get; set; }
        public int HMoveGS { get; set; }
        public int HealthGS { get; set; }
        public FieldPoint_DTO PointGS { get; set; }

        public string ToString => "Participant info: " + " Vertical Movement: " + VMoveGS + ", Horizontal Movement: " + HMoveGS + "\n" +
            "Name: " + NameGS + ", Health: " + HealthGS + ", Offence: " + OffenceGS + ", Defence: " + DefenceGS + ", Xpoint: " + PointGS.XPoint + ", Ypoint: " + PointGS.YPoint;
    }
}

//Ctrl + R + E (creates a get and set for a private variable
