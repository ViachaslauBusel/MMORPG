using Items;
using Protocol.Data.Items;
using Protocol.MSG.Game.Inventory;
using RUCP;
using Zenject;

namespace Cells
{
    public class DeletCell : Cell
    {
        private ItemUsageService _itemUsageService;
        private SelectQuantityWindow _selectQuantityWindow;

        [Inject]
        private void Construct(ItemUsageService itemUsageService, SelectQuantityWindow selectQuantityWindow)
        {
            _itemUsageService = itemUsageService;
            _selectQuantityWindow = selectQuantityWindow;
        }

        private new void Start()
        {
            Init();
            base.Start();
            icon.enabled = true;
        }

        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell is ItemCell itemCell)
            {
                if (itemCell.IsEmpty()) return;

                Item item = itemCell.GetItem();
                ItemStorageType deletFrom = itemCell.StorageType;

                if (item.Data.stack)
                {
                    _selectQuantityWindow.Subscribe(
                    "How many pieces to move?",
                    (count) =>
                    {
                        DestroyItemRequest(itemCell.GetItem(), deletFrom, count);
                    },
                    () => { }
                    );
                }
                else
                {
                    DestroyItemRequest(itemCell.GetItem(), deletFrom, 1);
                }
            }
        }

        private void DestroyItemRequest(Item item, ItemStorageType deletFrom, int count)
        {
            Confirm.Instance.Subscribe(
                   "Do you really want to delete the item?",
                   () =>
                   {
                       switch(deletFrom)
                       {
                           case ItemStorageType.PrimaryBag:
                           case ItemStorageType.SecondaryBag:
                               _itemUsageService.DestroyInventoryItem(item.UniqueID, count);
                               break;
                           case ItemStorageType.Equipment:
                               _itemUsageService.DestroyEquipmentItem(item.UniqueID);
                               break;
                       }
                   },
                    () => { });
        }

    }
}