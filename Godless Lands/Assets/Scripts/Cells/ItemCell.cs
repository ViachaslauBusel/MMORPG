﻿using Items;
using RUCP;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Cells
{


    public class ItemCell : Cell
    {
        protected Item _item;
        protected int index;
     //   protected int objectID;
        protected Text countTxt;
        private ItemUsageService _itemUsageService;


        [Inject]
        public void Construct(ItemUsageService itemUsageService)
        {
            _itemUsageService = itemUsageService;
        }

        protected new void Awake()
        {
            base.Awake();
            countTxt = transform.Find("Count").GetComponent<Text>();
        }

        public override bool IsEmpty()
        {
            if (_item == null) return true;
            if (_item.IsEmpty) return true;
            return false;
        }

        /// <summary>
        /// Send command to the server to use the item
        /// </summary>
        public override void Use()
        {
            if (IsEmpty()) return;
           // _itemUsageService.UseItem(_item.objectID);
        }

        /// <summary>
        /// Рюкзак -> сумка -> рюкзак
        /// </summary>
        public void Move()
        {
            if (IsEmpty()) return;
            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.ItemMove);
            //nw.WriteInt(item.objectID);
            //NetworkManager.Send(nw);
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
                if (countTxt != null)
                    countTxt.text = "";
                HideIcon();
                return;
            }

            ShowIcon();
            UpdateIcon();
            if (countTxt != null)
            {
                countTxt.text = _item.Data.stack ? _item.Count.ToString() : "";
            }
        }

        protected void UpdateIcon()
        {
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
            if(cell is ActionCell || cell is WorkbenchCell)
            {
                cell.Abort();
                return;
            }

            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null || itemCell.IsEmpty()) return;

            //TODO msg
            //Packet writer = new Packet(Channel.Reliable);
            //writer.WriteType(Types.WrapItem);
            //writer.WriteInt(itemCell.item.objectID);//Предмет который надо переместить
            //writer.WriteInt(index);//в ячейку индекс
            //NetworkManager.Send(writer);

        }

       // public void SetObjectID(int objetcID)
       // {
       //     item.objectID = objetcID;
       // }

        public void SetIndex(int index)
        {
            this.index = index;
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
            return index;
        }
        public override long GetObjectID()
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
            _item.Data.maxDurability = durability;
        }
    }
}