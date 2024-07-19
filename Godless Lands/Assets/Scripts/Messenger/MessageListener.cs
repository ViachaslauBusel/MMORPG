using Items;
using Network.Core;
using Protocol;
using Protocol.MSG.Game.Messenger;
using RUCP;
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
            message.Msg = ReplaceItemName(message.Msg);
           
            TabsManager.Instance.AddMessage(message);
        }

        private string ReplaceItemName(string msg)
        {
            Regex itemRegex = new Regex(@"%item_name:(\d+):(\d+)");
            MatchCollection matches = itemRegex.Matches(msg);

            foreach (Match match in matches)
            {
                string itemId = match.Groups[1].Value;
                string itemCount = match.Groups[2].Value;
                string itemName = _itemsFactory.CreateItem(int.Parse(itemId)).Data.Name;
                msg = msg.Replace(match.Value, $"{itemName} x {itemCount}");
            }

            return msg;
        }
        private void OnDestroy()
        {

            _networkManager?.UnregisterHandler(Opcode.MSG_MESSAGE_SC);
        }
    }
}