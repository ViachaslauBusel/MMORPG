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
    }
}
