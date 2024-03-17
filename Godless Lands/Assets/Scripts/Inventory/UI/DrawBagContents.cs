using Cells;
using Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Inventory.UI
{
    internal class DrawBagContents : MonoBehaviour
    {
        [SerializeField]
        private Transform _content;
        public Text text_filling;
        public BarWeight weight;
        public GameObject itemCell;
        private List<ItemCell> _cells;
        private Bag _bag;
        private ItemsFactory _itemsFactory;

        [Inject]
        private void Construct(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        public void Init(Bag bag)
        {
            _bag = bag;
            _bag.OnCapacityChanged += UpdateCellsCount;
            _bag.OnWeightChanged += UpdateWeght;
            _bag.OnItemsChanged += UpdateItems;
        }

        private void UpdateWeght()
        {
          
            weight.UpdateWeight(_bag.CurrentWeight, _bag.MaxWeight);
        }

        public void UpdateItems()
        {
            foreach (var itemData in _bag.Items)
            {
                if (itemData.SlotIndex >= 0 && itemData.SlotIndex < _cells.Count)
                {
                    if (itemData.SlotIndex >= 0 && IsNeedUpdate(itemData, _cells[itemData.SlotIndex].GetItem())) continue;
                    Debug.Log($"[DrawBag] UpdateItems: {itemData.SlotIndex}:{itemData.ItemID}");
                    Item newItem = itemData.ItemID != 0 ? _itemsFactory.CreateItem(itemData) : null;
                    _cells[itemData.SlotIndex].PutItem(newItem);
                }
                else Debug.LogError("Index out of range");
            }
        }

        private bool IsNeedUpdate(ItemNetworkData itemData, Item item)
        {
            int itemId = item?.id ?? 0;
            int itemCount = item?.count ?? 0;
            long itemUniqueId = item?.objectID ?? 0;
            return itemData.ItemID == itemId && itemData.Count == itemCount && itemData.UniqueID == itemUniqueId;
        }

        private void UpdateCellsCount()
        {
            text_filling.text = _bag.CurrentItemsCount + "/" + _bag.MaxItemsCount;

            _cells ??= new List<ItemCell>();
            int maxCell = _bag.MaxItemsCount;
            //Create empty cells
            for (int i = _cells.Count; i < maxCell; i++)
            {
                GameObject _obj = Instantiate(itemCell, _content);
                var itemCellComponent = _obj.GetComponent<ItemCell>();
                itemCellComponent.SetIndex(i);
                _cells.Add(itemCellComponent);
            }

            //Destroy cells
            while (_cells.Count > maxCell)
            {
                int lastIndex = _cells.Count - 1;
                Destroy(_cells[lastIndex].gameObject);
                _cells.RemoveAt(lastIndex);
            }
            Debug.Log($"[DrawBag] UpdateCellsCount:{_cells.Count}");
        }

        internal bool TryGetItemCellByObjectId(long objectId, out ItemCell item)
        {
            int itemIndex = _cells.FindIndex(cell => cell.GetItem() is Item item && item.objectID == objectId);
            item = itemIndex >= 0 ? _cells[itemIndex] : null;
            return itemIndex >= 0;
        }
    }
}
