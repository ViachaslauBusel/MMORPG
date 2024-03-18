using Protocol.MSG.Game.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public class ItemUsageService
    {
        private NetworkManager _networkManager;

        public ItemUsageService(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void UseItem(long objectID)
        {
            MSG_USE_ITEM msg = new MSG_USE_ITEM();
            msg.ItemUID = objectID;
            _networkManager.Client.Send(msg);
        }

        internal void SwampItem(MSG_SWAMP_ITEMS swamp_command)
        {
            _networkManager.Client.Send(swamp_command);
        }
        public void TransferItemToAnotherBag(long itemUID)
        {
            MSG_TRANSFER_ITEM_TO_ANOTHER_BAG msg = new MSG_TRANSFER_ITEM_TO_ANOTHER_BAG();
            msg.ItemUID = itemUID;
            _networkManager.Client.Send(msg);
        }
    }
}
