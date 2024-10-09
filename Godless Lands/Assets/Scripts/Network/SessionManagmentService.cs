using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
    public class SessionManagmentService
    {
        public int CharacterObjectID { get; private set; }
        public int CharacterID { get; private set; }

        public event Action<int> OnCharacterObjectIDChanged;
        public event Action<int> OnCharacterIDChanged;

        public void SetCharacterObjectID(int objID)
        {
            this.CharacterObjectID = objID;
            OnCharacterObjectIDChanged?.Invoke(objID);
        }

        internal void SetCharacterID(int characterID)
        {
            this.CharacterID = characterID;
            OnCharacterIDChanged?.Invoke(characterID);
        }
    }
}
