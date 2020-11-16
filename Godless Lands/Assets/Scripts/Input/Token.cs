using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Token
{
   
    public KeyCode key;
    public string name;

    public Token(KeyCode key, string name)
    {
        this.key = key;
        this.name = name;
    }
}
