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

        public int VMove1 { get => VMove; set => VMove = value; }
        public string Name { get => name; set => name = value; }
        public int Offence1 { get => Offence; set => Offence = value; }
        public int Defence1 { get => Defence; set => Defence = value; }
        public int HMove1 { get => HMove; set => HMove = value; }
        public int Health1 { get => Health; set => Health = value; }
    }
}

//Ctrl + R + E (creates a get and set for a private variable
