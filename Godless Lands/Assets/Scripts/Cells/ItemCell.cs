using Cells.CellStateCache;
using Items;
using Protocol.Data.Items;
using Protocol.MSG.Game.Inventory;
using RUCP;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cells
{


    public class ItemCell : Cell
    {
        protected Item _item;
        protected ItemStorageType _storageType;
        protected int _index;
        protected Text _countTxt;
        protected ItemUsageService _itemUsageService;
        protected CellStateCacheSystem _cellStateCacheSystem;

        public ItemStorageType StorageType => _storageType;

        [Inject]
        public void Construct(ItemUsageService itemUsageService, CellStateCacheSystem cellStateCacheSystem)
        {
            _itemUsageService = itemUsageService;
            _cellStateCacheSystem = cellStateCacheSystem;
        }

        public void Init(ItemStorageType storageType, int index)
        {
            _storageType = storageType;
            _index = index;

            base.Init();

            _countTxt = transform.Find("Count")?.GetComponent<Text>();
        }

        public override bool IsEmpty()
        {
            return _item == null || _item.IsEmpty;
        }

        /// <summary>
        /// Send command to the server to use the item
        /// </summary>
        public override void Use()
        {
            if (IsEmpty()) return;
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
            if (_countTxt != null)
            {
                _countTxt.text = _item.Data.stack ? _item.Count.ToString() : "";
            }
        }

        protected void UpdateIcon()
        {
            if(icon == null) return;
            icon.sprite = Sprite.Create(_item.Data.texture, new Rect(0.0f, 0.0f, _item.Data.texture.width, _item.Data.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
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
                PutItem(itemCell.GetItem());
                MSG_SWAMP_ITEMS swamp_command = new MSG_SWAMP_ITEMS();
                swamp_command.ItemUID = itemCell.GetItemUID();
                swamp_command.ToCellIndex = _index;
                _itemUsageService.SwampItem(swamp_command);
            }
        }

       // public void SetObjectID(int objetcID)
       // {
       //     item.objectID = objetcID;
       // }

        

        public Item GetItem()
        {
            return _item;
        }
        public int ID()
        {
            if (IsEmpty()) return -1;
            return _item.Data.id;
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
            if(!IsEmpty() && _item.Data.stack) return GetCount().ToString();
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
            if (_countTxt != null)
                _countTxt.text = _item.Data.stack ? _item.Count.ToString() : "";
        }
    }
}