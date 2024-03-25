using Items;
using Protocol.Data.Items.Network;
using RUCP;
using RUCP.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Drop
{
    public class DropWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cellItemDropPrefab;
        [SerializeField]
        private Transform _contentParent;
        private ItemsFactory _itemsFactory;
        private Canvas _canvas;
        private List<GameObject> _dropList;

        [Inject]
        public void Construct(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        private void Start()
        {
            _dropList = new();
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        internal void Open(List<ItemSyncData> items)
        {
            _canvas.enabled = true;
            ClearDropList();

            foreach (ItemSyncData item in items)
            {
                AddItem(item);
            }
        }

        private void AddItem(ItemSyncData item)
        {
            GameObject _obj = Instantiate(_cellItemDropPrefab);
            _obj.transform.SetParent(_contentParent);
            _obj.GetComponent<CellItemDrop>().SetItem(_itemsFactory.CreateItem(item.ItemID, count: item.Count, slotIndex: item.SlotIndex));
            _dropList.Add(_obj);
        }

        private void ClearDropList()
        {
            foreach (GameObject _obj in _dropList)
            {
                Destroy(_obj);
            }
            _dropList.Clear();
        }

        public void Close()
        {
            ClearDropList();
            _canvas.enabled = false;
        }
    }
}