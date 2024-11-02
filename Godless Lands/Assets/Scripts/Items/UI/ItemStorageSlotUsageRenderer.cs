using UnityEngine;
using UnityEngine.UI;
using Windows;

namespace Items.UI
{
    public class ItemStorageSlotUsageDisplay : WindowElement
    {
        [SerializeField]
        private Text _slotUsageText;
        private ItemStorage _itemStorage;
        private Window _parentWindow;

        public void Initialize(ItemStorage itemStorage)
        {
            if (ParentWindow.IsOpened)
            {
                HandleWindowClose();
            }

            _itemStorage = itemStorage;

            if (ParentWindow.IsOpened)
            {
                HandleWindowOpen();
            }
        }

        protected override void HandleWindowOpen()
        {
            _itemStorage.OnCapacityChanged += UpdateSlotUsageDisplay;
            UpdateSlotUsageDisplay(_itemStorage.CurrentItemsCount, _itemStorage.MaxItemsCount);
        }

        protected override void HandleWindowClose()
        {
            _itemStorage.OnCapacityChanged -= UpdateSlotUsageDisplay;
        }

        private void UpdateSlotUsageDisplay(int currentCount, int maxCount)
        {
            _slotUsageText.text = currentCount + "/" + maxCount;
        }
    }
}

