using Items;
using Protocol.Data.Items;
using UI.ConfirmationDialog;
using Zenject;

namespace Cells
{
    public class DeletCell : Cell
    {
        private ItemUsageService _itemUsageService;
        private SelectQuantityWindow _selectQuantityWindow;
        private ConfirmationDialogController _confirmationDialog;

        [Inject]
        private void Construct(ItemUsageService itemUsageService, SelectQuantityWindow selectQuantityWindow, ConfirmationDialogController confirmationDialog)
        {
            _itemUsageService = itemUsageService;
            _selectQuantityWindow = selectQuantityWindow;
            _confirmationDialog = confirmationDialog;
        }

        private new void Start()
        {
            Init();
            base.Start();
            _icon.enabled = true;
        }

        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell is ItemCell itemCell)
            {
                if (itemCell.IsEmpty()) return;

                Item item = itemCell.GetItem();
                ItemStorageType deletFrom = itemCell.StorageType;

                if (item.Data.IsStackable)
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
            _confirmationDialog.AddRequest(
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