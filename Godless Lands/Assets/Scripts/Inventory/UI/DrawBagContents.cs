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
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
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
            foreach (var item in _bag.Items)
            {
                if (item.SlotIndex >= 0 && item.SlotIndex < _cells.Count)
                {
                    var existingItem = _cells[item.SlotIndex].GetItem();
                    if (ShouldUpdateItem(item, existingItem))
                    {
                        _cells[item.SlotIndex].PutItem(item);
                    }
                }
                else Debug.LogError("Index out of range");
            }
        }

        private bool ShouldUpdateItem(Item itemData, Item item)
        {
            return itemData != item;
        }

        private void UpdateCellsCount()
        {
            text_filling.text = _bag.CurrentItemsCount + "/" + _bag.MaxItemsCount;

            _cells ??= new List<ItemCell>();
            int maxCell = _bag.MaxItemsCount;
            //Create empty cells
            for (int i = _cells.Count; i < maxCell; i++)
            {
                GameObject _obj = _diContainer.InstantiatePrefab(itemCell, _content);
                var itemCellComponent = _obj.GetComponent<ItemCell>();
                itemCellComponent.Init(_bag.StorageType, i);
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

        internal bool TryGetItemCellByItemUID(long itemUID, out ItemCell item)
        {
            int itemIndex = _cells.FindIndex(cell => cell.GetItem() is Item item && item.UniqueID == itemUID);
            item = itemIndex >= 0 ? _cells[itemIndex] : null;
            return itemIndex >= 0;
        }
    }
}
