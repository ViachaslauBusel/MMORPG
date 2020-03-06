using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
  
    
    public class ItemCell : Cell
    {
        protected Item item;
        protected int index;
        protected int objectID;
        protected Text countTxt;



        protected new void Awake()
        {
            base.Awake();
            countTxt = transform.Find("Count").GetComponent<Text>();
        }

        public override bool IsEmpty()
        {
            if (item == null) return true;
            if (item.id <= 0) return true;
            return false;
        }

        public override void Use()
        {
            if (IsEmpty()) return;
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.UseItem);
            nw.write(objectID);
            nw.write(item.id);
            NetworkManager.Send(nw);
        }

        public void Move()
        {
            if (IsEmpty()) return;
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.ItemMove);
            nw.write(objectID);
            NetworkManager.Send(nw);
        }

        public virtual void PutItem(Item item, int count)
        {
           
            this.item = item;
            if (IsEmpty())
            {
                if (countTxt != null)
                    countTxt.text = "";
                HideIcon();
                return;
            }
            item.count = count;
            ShowIcon();
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            if (countTxt != null)
            {
                if (item.stack)
                    countTxt.text = count.ToString();
                else countTxt.text = "";
            }
        }

        public void Refresh(Item item, int count, int key)
        { 
            PutItem(item, count);
            this.objectID = key;

        }

        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if(cell.GetType() == typeof(ArmorCell))
            {
                cell.Use();
            }
            if (cell.GetType() != typeof(ItemCell)) return;
            ItemCell itemCell = cell as ItemCell;
            NetworkWriter writer = new NetworkWriter(Channels.Reliable);
            writer.SetTypePack(Types.WrapItem);
            writer.write(itemCell.objectID);
            writer.write((byte)index);
            NetworkManager.Send(writer);

        }

        public void SetObjectID(int objetcID)
        {
            this.objectID = objetcID;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public Item GetItem()
        {
            return item;
        }
        public int ID()
        {
            if (IsEmpty()) return -1;
            return item.id;
        }
        public int GetCount()
        {
            if (IsEmpty()) return 0;
            return item.count;
        }
        public int GetIndex()
        {
            return index;
        }
        public int GetObjectID()
        {
            return objectID;
        }

        public void SetEnchantLevel(int level)
        {
            item.enchant_level = level;
        }
        public void SetDurabilty(int durability)
        {
            item.durability = durability;
        }
        public void SetMaxDurabilty(int durability)
        {
            item.maxDurability = durability;
        }
    }
}