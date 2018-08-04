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
        Field_Impl fImpl;
        #endregion


        #region constructor
        public Game_Logic_Impl()
        { 
            pImpl = new Participant_Impl(); 
            field = new Field_DTO();
            fImpl = new Field_Impl();
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
            fImpl.AddParticipantToField(pDTO);
        } 
        
        //returns field
        public List<FieldPoint_DTO> GetField() 
        { 
            return field.FieldGS;
        }

        //handles contact between game logic and logic related to participants 
        //attempt to not have any objects created in this folder (allows for flexibility) 
        public void MoveParticipant(Participant_DTO pDTO) 
        {
            pImpl.UpdateFieldToParticipant(pDTO); //directly throws participant object 
            //AddParticipantToField(pImpl.GetParticipant(Participant_name)); //directly throws participant object
        }

        public void EmptyField()
        {
            fImpl.EmptyField();
        }

        public void AddPointToField(FieldPoint_DTO fpDTO)
        {
            fImpl.AddPointToField(fpDTO);
        }
        #endregion
    } 
} 
