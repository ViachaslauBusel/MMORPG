using System.Linq;
using TMPro;
using UnityEngine;
using Windows;

namespace Shop.UI
{
    public abstract class TotalItemPriceCalculator : ParentWindowElement
    {
        [SerializeField]
        private TMP_Text _totalPriceText;
        private ItemStorage _itemStorage;

        public void SetItemStorage(ItemStorage itemStorage)
        {
            if (_itemStorage != null)
            {
                _itemStorage.OnItemsChanged -= OnItemsChanged;
            }

            _itemStorage = itemStorage;

            if (_itemStorage != null)
            {
                _itemStorage.OnItemsChanged += OnItemsChanged;
                OnItemsChanged();
            }
        }

        private void OnItemsChanged()
        {
            Debug.Log("OnItemsChanged");
            int totalPrice = _itemStorage.Items.Where(item => item?.Data != null).Sum(item => item.Data.Price * item.Count);
            _totalPriceText.text = totalPrice.ToString();
        }
    }
}
