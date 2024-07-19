using Network.Core;
using Protocol.Data.Items;
using Protocol.MSG.Game.Equipment.MSG;
using Protocol.MSG.Game.Inventory;

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

        internal void TakeFromEquipAndPutInBag(ItemStorageType storageType, int index, long itemUID)
        {
            MSG_UNEQUIP_ITEM_TO_INVENTORY_CS msg = new MSG_UNEQUIP_ITEM_TO_INVENTORY_CS();
            msg.ItemUID = itemUID;
            msg.ToStorageType = storageType;
            msg.ToCellIndex = index;
            _networkManager.Client.Send(msg);
        }

        internal void DestroyInventoryItem(long uniqueID, int count)
        {
            MSG_DESTROY_ITEM_INVENTORY_CS msg = new MSG_DESTROY_ITEM_INVENTORY_CS();
            msg.ItemUID = uniqueID;
            msg.ItemCount = count;
            _networkManager.Client.Send(msg);
        }

        internal void DestroyEquipmentItem(long uniqueID)
        {
            MSG_DESTROY_ITEM_EQUIPMENT_CS msg = new MSG_DESTROY_ITEM_EQUIPMENT_CS();
            msg.ItemUID = uniqueID;
            _networkManager.Client.Send(msg);
        }
    }
}
