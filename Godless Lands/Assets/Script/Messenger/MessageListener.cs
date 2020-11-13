using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using UnityEngine.UI;
using Items;
using RUCP.Packets;
using System.Linq;
using System.Text.RegularExpressions;

namespace Messenger
{
    public class MessageListener : MonoBehaviour
    {

        private void Awake()
        {
            HandlersStorage.RegisterHandler(Types.ChatMessage, Receiving);
        }


        private void Receiving(Packet nw)
        {

            Message message = new Message();
            message.Layer = (MsgLayer)nw.ReadByte();
            message.CharName = nw.ReadString();
            message.Msg = nw.ReadString();

            //    if (_message.Contains("%item:")) _message.Where((a))
            message.Msg = ReplaceItem(message.Msg);
           
            TabsManager.Instance.AddMessage(message);
        }

        private string ReplaceItem(string msg)
        {
            Regex regex = new Regex(@"%item:\d+");
            MatchCollection match = regex.Matches(msg);
            foreach (Match str in match)
            {
                print("str: " + str.Value);
                regex = new Regex(@"\d+");
                string number = regex.Match(str.Value).Value;
                print("number: " + number);
                msg = msg.Replace(
                    str.Value,
                    ItemsManager.Create(Int32.Parse(number)).nameItem
                           );
            }
            return msg;
        }
        private void OnDestroy()
        {

            HandlersStorage.UnregisterHandler(Types.ChatMessage);
        }
    }
}