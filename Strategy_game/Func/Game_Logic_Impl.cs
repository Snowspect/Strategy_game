using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy_game.Data.DTO;
using Strategy_game.Data;

namespace Strategy_game.Func
{
    class Game_Logic_Impl
    {
        Field_DTO field;
        Participant_Impl pImpl;
        FieldPoint_DTO fp_DTO;
        public Game_Logic_Impl()
        { 
            pImpl = new Participant_Impl(); 
            field = new Field_DTO(); 
        } 

        // Adds a participant to the field
        public void AddParticipantToField(Participant_DTO pDTO)
        { 
            field.FieldGS.Add(Tuple.Create(pDTO,pDTO.PointGS)); //using tuple due to dictionary only containing unique sets.
        } 
        
        //returns field
        public List<Tuple<Participant_DTO, FieldPoint_DTO>> GetField() 
        { 
            return field.FieldGS; 
        } 

        //handles contact between game logic and logic related to participants 
        //attempt to not have any objects created in this folder (allows for flexibility) 
        public void MoveParticipant(int xCoord, int yCoord, string Participant_name) 
        { 
            fp_DTO = new FieldPoint_DTO(); 
            fp_DTO.XPoint = xCoord; fp_DTO.YPoint = yCoord; 
            pImpl.UpdateFieldToParticipant(fp_DTO, pImpl.GetParticipant(Participant_name)); //directly throws participant object 
            AddParticipantToField(pImpl.GetParticipant(Participant_name)); //directly throws participant object

        }

        public string GetImage(string participant_name)
        { 
            return pImpl.getImageFromParticipant(participant_name);
        }
        public string GetParticipantFieldCoord(string participantToMove)
        {
            return "x1y1";
        }
    } 
} 
