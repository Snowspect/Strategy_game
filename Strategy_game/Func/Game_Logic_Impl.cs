using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy_game.Data.DTO;
using Strategy_game.Data;
using Strategy_game.Data.Interface_Impl;

namespace Strategy_game.Func
{
    public class Game_Logic_Impl : IGameLogic_Impl<Participant_DTO, FieldPoint_DTO, int, string>
    {
        #region localVariables
        Field_DTO field;
        Participant_Impl pImpl;
        FieldPoint_DTO fp_DTO;
        #endregion

        #region constructor
        public Game_Logic_Impl()
        { 
            pImpl = new Participant_Impl(); 
            field = new Field_DTO(); 
        }
        #endregion

        /*
         * AddParticipantToField
         * GetField
         * MoveParticipant
         * GetImage (asociated with participant)
         * GetParticipantFieldCoord
         */
        #region methods
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
            Console.WriteLine("GL Test: " + Participant_name);
            fp_DTO = new FieldPoint_DTO(); 
            fp_DTO.XPoint = xCoord; fp_DTO.YPoint = yCoord; 
            pImpl.UpdateFieldToParticipant(fp_DTO, pImpl.GetParticipant(Participant_name)); //directly throws participant object 
            //AddParticipantToField(pImpl.GetParticipant(Participant_name)); //directly throws participant object
        }
        //Retrieves the image from a participant
        public string GetImage(string participant_name)
        { 
            return pImpl.getImageFromParticipant(participant_name);
        }
        //Retrieves the x and y coordinates related to a participant and returns it as a string name
        public string GetParticipantFieldCoord(string participant_name)
        {
            foreach (var item in GetField())
            {
                if (item.Item1.NameGS == participant_name)
                {
                    return "x" + item.Item1.PointGS.XPoint + "y" + item.Item1.PointGS.YPoint;
                }
            }
            return null;
        }
        #endregion
    } 
} 
