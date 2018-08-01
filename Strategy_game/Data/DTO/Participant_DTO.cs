using Strategy_game.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data
{
    /// <summary>
    /// Holds info regarding one participant
    /// </summary>
    public class Participant_DTO
    {
        public Participant_DTO()
        {
            HealthGS = 1;
            DefenceGS = 1;
            OffenceGS = 1;
            NameGS = "test";
            VMoveGS = 1;
            HMoveGS = 1;
            TeamGS = "none";
            ImageGS = "SlimeBlack.png";
            PointGS = new FieldPoint_DTO();
            PointGS.XPoint = 0;
            PointGS.YPoint = 0;
            StrongAgainstGS = new List<string>();
            WeakAgainstGS = new List<string>();
            ImmuneAgainstGS = new List<string>();
        }
        public Participant_DTO(int h, int d, int o, int Vm, int Hm, string n, string teamName, string stA1, string stA2, string wkA1, string wkA2, string imA1, string imA2)
        {
            HealthGS = h;
            DefenceGS = d;
            OffenceGS = o;
            NameGS = n;
            VMoveGS = Vm;
            HMoveGS = Hm;
            TeamGS = "none";
            TeamColorGS = "none";
            ImageGS = "SlimeBlack.png";
            PointGS = new FieldPoint_DTO();
            PointGS.XPoint = 0;
            PointGS.YPoint = 0;
            StrongAgainstGS = new List<string>();
            StrongAgainstGS.Add(stA1); StrongAgainstGS.Add(stA2);
            WeakAgainstGS = new List<string>();
            WeakAgainstGS.Add(wkA1); WeakAgainstGS.Add(wkA2);
            ImmuneAgainstGS = new List<string>();
            ImmuneAgainstGS.Add(imA1); ImmuneAgainstGS.Add(imA2);
        }

        public int VMoveGS { get; set; }
        public string NameGS { get; set; }
        public int OffenceGS { get; set; }
        public int DefenceGS { get; set; }
        public int HMoveGS { get; set; }
        public int HealthGS { get; set; }
        public string TeamGS { get; set; }
        public List<string> StrongAgainstGS { get; set; }
        public List<string> WeakAgainstGS { get; set; }
        public List<string> ImmuneAgainstGS { get; set; }
        public FieldPoint_DTO PointGS { get; set; }
        public string ImageGS { get; set; }
        public string TeamColorGS { get; set; }

        public string GetToString()
        {
            return "Participant info: " + " Vertical Movement: " + VMoveGS + ", Horizontal Movement: " + HMoveGS + "\n" +
            "Name: " + NameGS + ", Health: " + HealthGS + ", Offence: " + OffenceGS + ", Defence: " + DefenceGS + ", Xpoint: " + PointGS.XPoint + ", Ypoint: " + PointGS.YPoint + "\n"
            + "Team: " + TeamGS + ", Strong Against: " + StrongAgainstGS[0] + "," + StrongAgainstGS[1] + ", Weak Against: " +
            WeakAgainstGS[0] + "," + WeakAgainstGS[1] + ", Immune Against: " + ImmuneAgainstGS[0] + "," + ImmuneAgainstGS[1];
        }
    }
}

//Ctrl + R + E (creates a get and set for a private variable
