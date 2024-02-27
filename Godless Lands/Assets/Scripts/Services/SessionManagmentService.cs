using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManagmentService 
{
   public int CharacterObjectID { get; private set; }

    public event Action<int> OnCharacterObjectIDChanged;

    public void SetCharacterObjectID(int objID)
    {
        this.CharacterObjectID = objID;
        OnCharacterObjectIDChanged?.Invoke(objID);
    }
}
