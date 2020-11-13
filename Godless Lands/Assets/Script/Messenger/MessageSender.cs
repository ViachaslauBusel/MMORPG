using RUCP.Network;
using RUCP.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger
{
    class MessageSender: MonoBehaviour
    {
        private InputField inputField;
        private void Awake()
        {
            inputField = GetComponent<InputField>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Return) && inputField.text.Length > 0)
            {
                Packet nw = new Packet(Channel.Reliable);
                nw.WriteType(Types.ChatMessage);
                nw.WriteString(inputField.text);

                NetworkManager.Send(nw);
                inputField.text = "";
            }
        }
    }
}
