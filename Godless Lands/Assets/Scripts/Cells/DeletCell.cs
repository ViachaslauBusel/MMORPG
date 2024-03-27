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

        [Inject]
        private void Construct(ItemUsageService itemUsageService)
        {
            _itemUsageService = itemUsageService;
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
                    SelectQuantity.Instance.Subscribe(
                    "How many pieces to move?",
                    (count) =>
                    {
                        DestroyItemRequest(itemCell.GetItem(), deletFrom);
                    },
                    () => { }
                    );
                }
                else
                {
                    DestroyItemRequest(itemCell.GetItem(), deletFrom);
                }
            }
        }

        private void DestroyItemRequest(Item item, ItemStorageType deletFrom)
        {
            Confirm.Instance.Subscribe(
                   "Do you really want to delete the item?",
                   () =>
                   {
                       switch(deletFrom)
                       {
                           case ItemStorageType.PrimaryBag:
                           case ItemStorageType.SecondaryBag:
                               _itemUsageService.DestroyInventoryItem(item.UniqueID);
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