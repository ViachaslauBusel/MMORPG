using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat_MessageSend : MonoBehaviour {

    private InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void SenderMessage()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.ChatMessage);
            nw.write(inputField.text);

            NetworkManager.Send(nw);
            inputField.text = "";
        }
        
    }
}
