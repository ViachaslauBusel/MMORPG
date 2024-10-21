using UnityEngine;
using UnityEngine.UI;
using Windows;

namespace Items.UI
{
    public class ItemStorageWeightDisplay : ParentWindowElement
    {
        [SerializeField]
        private Image _weightBar;
        [SerializeField]
        private Text _weightText;
        private ItemStorage _itemStorage;

        public void Initialize(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        protected override void HandleWindowOpen()
        {
            _itemStorage.OnWeightChanged += RefreshWeightDisplay;
            RefreshWeightDisplay(_itemStorage.CurrentWeight, _itemStorage.MaxWeight);
        }

        protected override void HandleWindowClose()
        {
            _itemStorage.OnWeightChanged -= RefreshWeightDisplay;
        }

        private void RefreshWeightDisplay(int weight, int maxWeight)
        {
            _weightBar.fillAmount = weight / (float)maxWeight;
            _weightText.text = weight + "/" + maxWeight;
        }
    }
}
