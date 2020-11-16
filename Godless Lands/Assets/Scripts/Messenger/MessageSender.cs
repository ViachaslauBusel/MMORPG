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
        [SerializeField] InputField inputField;
        [SerializeField] Dropdown dropdown;


        private void Update()
        {
            if (Input.GetKey(KeyCode.Return))
            {
                if (inputField.isFocused)
                {
                    if (inputField.text.Length == 0) return;
                    MsgLayer layer;
                    switch (dropdown.value)
                    {
                        case 0:
                            layer = MsgLayer.AroundMsg;
                            break;
                        case 1:
                            layer = MsgLayer.AllMsg;
                            break;
                        default:
                            layer = MsgLayer.AroundMsg;
                            break;
                    }
                    Packet nw = new Packet(Channel.Reliable);
                    nw.WriteType(Types.ChatMessage);
                    nw.WriteByte((byte)layer);
                    nw.WriteString(inputField.text);

                    NetworkManager.Send(nw);
                    inputField.text = "";
                }
                else
                {
                    inputField.Select();
                    inputField.ActivateInputField();
                }
            }
        }
    }
}
