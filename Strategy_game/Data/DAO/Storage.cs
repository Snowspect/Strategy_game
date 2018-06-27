﻿using Strategy_game.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy_game.Data.DAO
{
    class Storage
    {
        public static List<Participant_DTO> StParticipant = new List<Participant_DTO>();

        //adds a participant to the storage /participant represnet one fighter on the field"
        public void AddToLayer(Participant_DTO pDTO)
        {
                StParticipant.Add(pDTO);
        }
        //Gets lists of participants
        public List<Participant_DTO> GetParticipantList()
        {
            return StParticipant;
        }
        //Adds field to a specific participant in storage
        public void AddFieldToParticipant(Participant_DTO pDTO, FieldPoint_DTO fpDTO)
        {
            foreach (var element in StParticipant) if(element.Equals(pDTO)) element.PointGS = fpDTO;
        }
    }
}
