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
            this.HealthGS = 1;
            this.DefenceGS = 1;
            this.OffenceGS = 1;
            this.NameGS = "test";
            this.VMoveGS = 1;
            this.HMoveGS = 1;
        }
        public Participant_DTO(int h, int d, int o, int Vm, int Hm, String n)
        {
            this.HealthGS = h;
            this.DefenceGS = d;
            this.OffenceGS = o;
            this.NameGS = n;
            this.VMoveGS = Vm;
            this.HMoveGS = Hm;
        }

        public int VMoveGS { get; set; }
        public string NameGS { get; set; }
        public int OffenceGS { get; set; }
        public int DefenceGS { get; set; }
        public int HMoveGS { get; set; }
        public int HealthGS { get; set; }

        public string ToString => "Participant info: " + " Vertical Movement: " + VMoveGS + ", Horizontal Movement: " + HMoveGS + "\n" +
            "Name: " + NameGS + ", Health: " + HealthGS + ", Offence: " + OffenceGS + ", Defence: " + DefenceGS;
    }
}

//Ctrl + R + E (creates a get and set for a private variable
