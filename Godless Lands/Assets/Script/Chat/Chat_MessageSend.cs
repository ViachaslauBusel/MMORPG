using RUCP;
using RUCP.Network;
using RUCP.Packets;
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
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.ChatMessage);
            nw.WriteString(inputField.text);

            NetworkManager.Send(nw);
            inputField.text = "";
        }
        
    }
}
