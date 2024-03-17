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

        public void UseItem(int objectID)
        {
            MSG_USE_ITEM msg = new MSG_USE_ITEM();
            msg.ItemUID = objectID;
            _networkManager.Client.Send(msg);
        }
    }
}
