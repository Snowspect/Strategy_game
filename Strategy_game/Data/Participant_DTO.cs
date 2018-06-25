using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    class Participant_DTO
    {
        private int Health;
        private int Defence;
        private int Offence;
        private String name;
        private int VMove;
        private int HMove;

        public Participant_DTO()
        {
            this.Health = 1;
            this.Defence = 1;
            this.Offence = 1;
            this.name = "test";
            this.VMove = 1;
            this.HMove = 1;
        }

        public int VMoveGS { get => VMove; set => VMove = value; }
        public string NameGS { get => name; set => name = value; }
        public int OffenceGS { get => Offence; set => Offence = value; }
        public int DefenceGS { get => Defence; set => Defence = value; }
        public int HMoveGS { get => HMove; set => HMove = value; }
        public int HealthGS { get => Health; set => Health = value; }

        public string ToString => "Participant info: " + " Vertical Movement: " + VMove + ", Horizontal Movement: " + HMove + "\n" +
            "Name: " + name + ", Health: " + Health + ", Offence: " + Offence + ", Defence: " + Defence;
    }
}

//Ctrl + R + E (creates a get and set for a private variable
