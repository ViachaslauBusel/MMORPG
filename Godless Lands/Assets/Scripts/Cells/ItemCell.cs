using Items;
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
        protected int _index;
        protected Text _countTxt;
        private ItemUsageService _itemUsageService;


        [Inject]
        public void Construct(ItemUsageService itemUsageService)
        {
            _itemUsageService = itemUsageService;
        }

        protected new void Awake()
        {
            base.Awake();
            _countTxt = transform.Find("Count").GetComponent<Text>();
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
                if (_countTxt != null)
                    _countTxt.text = "";
                HideIcon();
                return;
            }

            ShowIcon();
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

        /* public void Refresh(Item item, int key)
          { 
              PutItem(item);
              item.objectID = key;

          }*/

        /// <summary>
        /// wrap поменять содержимое ячеек местами
        /// </summary>
        /// <param name="cell"></param>
        public override void Put(Cell cell)
        {
            if (cell == null) return;
            /* if(cell.GetType() == typeof(ArmorCell))//Если вторая ячейка ячейка экепировки
             {
                 UnityEngine.Debug.Log("armor cell");
                 cell.Use();
                 return;
             }*/
            if (cell is ActionCell || cell is WorkbenchCell)
            {
                cell.Abort();
                return;
            }

            if (cell is ItemCell itemCell && itemCell.IsEmpty() == false)
            {
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

        public void SetIndex(int index)
        {
            this._index = index;
        }

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
    }
}