using Items;
using Protocol;
using Protocol.MSG.Game.Messenger;
using RUCP;
using RUCP.Handler;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using Zenject;

namespace Messenger
{
    public class MessageListener : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private ItemsFactory _itemsFactory;

        [Inject]
        private void Constrcut(NetworkManager networkManager, ItemsFactory itemsFactory)
        {
            _networkManager = networkManager;
            _itemsFactory = itemsFactory;

            networkManager.RegisterHandler(Opcode.MSG_MESSAGE_SC, Receiving);
        }



        private void Receiving(Packet packet)
        {
            packet.Read(out MSG_MESSAGE_SC msg);
            Message message = new Message();
            message.Layer = msg.Layer;
            message.CharName = msg.SenderName;
            message.Msg = msg.Message;

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
                    _itemsFactory.CreateItem(Int32.Parse(number)).Data.nameItem
                           );
            }
            return msg;
        }
        private void OnDestroy()
        {

            _networkManager?.UnregisterHandler(Types.ChatMessage);
        }
    }
}