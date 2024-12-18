﻿using Items;
using Protocol.Data.Items;
using Protocol.MSG.Game.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cells
{
    public class ItemCell : Cell
    {
        protected Image _lock;
        protected Item _item;
        protected ItemStorageType _storageType;
        protected int _index;
        protected Text _countTxt;
        protected ItemUsageService _itemUsageService;

        public ItemStorageType StorageType => _storageType;

        [Inject]
        public void Construct(ItemUsageService itemUsageService)
        {
            _itemUsageService = itemUsageService;
        }

        public void Init(ItemStorageType storageType, int index)
        {
            _storageType = storageType;
            _index = index;

            base.Init();

            _countTxt = transform.Find("Count")?.GetComponent<Text>();
            _lock = transform.Find("Lock")?.GetComponent<Image>();
        }

        public override bool IsEmpty()
        {
            return _item == null || _item.IsEmpty;
        }

        public override bool IsInteractingWithCurrentCell(Cell cell)
        {
            return cell.GetType() == typeof(ArmorCell) || cell.GetType() == typeof(ItemCell);
        }

        /// <summary>
        /// Send command to the server to use the item
        /// </summary>
        public override void Use()
        {
            if (IsEmpty() || IsLocked()) return;
            _itemUsageService.UseItem(_item.UniqueID);
        }

        /// <summary>
        /// Рюкзак -> сумка -> рюкзак
        /// </summary>
        public void TransferItemToAnotherBag()
        {
            if (IsEmpty()) return;

            _itemUsageService.TransferItemToAnotherBag(_item.UniqueID);
        }

        /// <summary>
        /// Draw the icon and the count of the item
        /// </summary>
        /// <param name="item"></param>
        public virtual void PutItem(Item item)
        {
            _item = item;
            if (IsEmpty())//If the cell is empty
            {
                Hide();
                return;
            }

            Show();
            UpdateIcon();
        }

        protected void UpdateIcon()
        {
            if(_icon == null) return;
            _icon.sprite = Sprite.Create(_item.Data.Icon, new Rect(0.0f, 0.0f, _item.Data.Icon.width, _item.Data.Icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        protected virtual void UpdateCount()
        {
            if (_countTxt != null)
            {
                _countTxt.text = _item.Data.IsStackable ? _item.Count.ToString() : "";
            }
        }

        /// <summary>
        /// wrap поменять содержимое ячеек местами
        /// </summary>
        /// <param name="cell"></param>
        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell.GetType() == typeof(ArmorCell))//If the second cell is an equipment cell,
                                                    //take item from cell and put it in the backpack in the specified cell.
            {
                if (cell.IsEmpty()) return;
                _itemUsageService.TakeFromEquipAndPutInBag(_storageType, _index, cell.GetItemUID());
                return;
            }

            if (cell is SmelterCell || cell is RecipeInputCell)
            {
                cell.Abort();
                return;
            }

            if (cell is ItemCell itemCell && itemCell.IsEmpty() == false)
            {
                //_cellStateCacheSystem.HideItemUntilResponse(itemCell, 1_500);
                //_cellStateCacheSystem.ShowItemUntilResponse(this, itemCell.GetItem(), 1_500);
                PutItem(itemCell.GetItem()); itemCell.Hide();
                MSG_SWAMP_ITEMS swamp_command = new MSG_SWAMP_ITEMS();
                swamp_command.ItemUID = itemCell.GetItemUID();
                swamp_command.ToCellIndex = _index;
                _itemUsageService.SwampItem(swamp_command);
            }
        }

        public Item GetItem()
        {
            return _item;
        }
        public int ID()
        {
            if (IsEmpty()) return -1;
            return _item.Data.ID;
        }
        public int GetCount()
        {
            if (IsEmpty()) return 0;
            return _item.Count;
        }
        public int GetIndex()
        {
            return _index;
        }
        public override long GetItemUID()
        {
            if (IsEmpty()) return 0;
            return _item.UniqueID;
        }

        public override string GetText()
        {
            if(!IsEmpty() && _item.Data.IsStackable) return GetCount().ToString();
            return base.GetText();
        }

        public void SetEnchantLevel(int level)
        {
            _item.EnchantLevel = level;
        }
        public void SetDurabilty(int durability)
        {
            _item.Durability = durability;
        }
        public void SetMaxDurabilty(int durability)
        {
          //  _item.Data.maxDurability = durability;
        }
        public override void Hide()
        {
            base.Hide();
            if (_countTxt != null)
                _countTxt.text = "";
        }
        public override void Show()
        {
            base.Show();
            UpdateCount();
        }

        internal void SetLock(bool value)
        {
            _locked = value;
           if(_lock != null) _lock.enabled = value;
        }
    }
}