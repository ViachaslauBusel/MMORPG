using Cells;
using System;
using System.Collections.Generic;
using UnityEngine;
using Windows;
using Zenject;

namespace Items.UI
{
    public class ItemStorageRenderer : ParentWindowElement
    {
        [SerializeField]
        private Transform _contentTransform;
        [SerializeField]
        private GameObject _itemCellPrefab;
        private List<ItemCell> _itemCells = new List<ItemCell>();
        private ItemStorage _itemStorage;
        private DiContainer _dependencyContainer;
        private Window _parentWindow;

        [Inject]
        private void Construct(DiContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer;
        }

        public void Initialize(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;

            if (ParentWindow.IsOpened)
            {
                RefreshCellCount(_itemStorage.CurrentItemsCount, _itemStorage.MaxItemsCount);
                RefreshItems();
            }
        }

        protected override void HandleWindowOpen()
        {
            Debug.Log($"[ItemStorageRenderer] HandleWindowOpen");
            _itemStorage.OnItemsChanged += RefreshItems;
            _itemStorage.OnCapacityChanged += RefreshCellCount;
            _itemStorage.OnLockUpdate += RefreshItems;

            RefreshCellCount(_itemStorage.CurrentItemsCount, _itemStorage.MaxItemsCount);
            RefreshItems();
        }

        protected override void HandleWindowClose()
        {
            _itemStorage.OnItemsChanged -= RefreshItems;
            _itemStorage.OnCapacityChanged -= RefreshCellCount;
            _itemStorage.OnLockUpdate -= RefreshItems;
        }

        public void RefreshItems()
        {
            foreach (var item in _itemStorage.Items)
            {
                if (item.SlotIndex >= 0 && item.SlotIndex < _itemCells.Count)
                {
                    var existingItem = _itemCells[item.SlotIndex].GetItem();
                    if (IsNeedsItemUpdate(item, existingItem))
                    {
                        _itemCells[item.SlotIndex].PutItem(item);
                    }
                    _itemCells[item.SlotIndex].SetLock(_itemStorage.IsItemLocked(item.UniqueID));
                }
                else Debug.LogError("Index out of range");
            }
        }

        private bool IsNeedsItemUpdate(Item itemData, Item existingItem)
        {
            return itemData != existingItem;
        }

        private void RefreshCellCount(int currentCount, int maxCount)
        {
            int maxCell = maxCount;
            //Create empty cells
            for (int i = _itemCells.Count; i < maxCell; i++)
            {
                GameObject _obj = _dependencyContainer.InstantiatePrefab(_itemCellPrefab, _contentTransform);
                var itemCellComponent = _obj.GetComponent<ItemCell>();
                itemCellComponent.Init(_itemStorage.StorageType, i);
                _itemCells.Add(itemCellComponent);
            }

            //Destroy cells
            while (_itemCells.Count > maxCell)
            {
                int lastIndex = _itemCells.Count - 1;
                Destroy(_itemCells[lastIndex].gameObject);
                _itemCells.RemoveAt(lastIndex);
            }
            //Debug.Log($"[DrawBag] RefreshCellCount:{_itemCells.Count}");
        }
    }
}

