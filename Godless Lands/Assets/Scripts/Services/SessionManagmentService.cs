using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManagmentService 
{
   public int CharacterObjectID { get; private set; }

    public void SetCharacterObjectID(int objID)
    {
        this.CharacterObjectID = objID;
    }
}
