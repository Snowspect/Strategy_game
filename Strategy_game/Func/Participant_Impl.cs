using Strategy_game.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Func
{
    
    class Participant_Impl
    {
        Participant_DTO pDTO = null;
        public Participant_Impl(Participant_DTO pDTO)
        {
            this.pDTO = pDTO;
            pDTO.HealthGS = 100;
            pDTO.DefenceGS = 4;
            pDTO.OffenceGS = 4;
            pDTO.NameGS = "Destroyer";
            pDTO.HMoveGS = 2;   
            pDTO.VMoveGS = 2;   
        }
        public string ToString => pDTO.ToString; //using getter on toString, so therefore not a method ()
    }
}
