using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class ItemCell : Cell
    {
        protected Item item;
        protected int index;
        protected int count;

        public override bool IsEmpty()
        {
            if (item == null) return true;
            if (item.id < 0) return true;
            return false;
        }

        public override void Use()
        {
            if (IsEmpty()) return;
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.UseItem);
            nw.write(index);
            nw.write(item.id);
            NetworkManager.Send(nw);
        }

        public void PutItem(Item item)
        {
            this.item = item;
            if (IsEmpty())
            {
                HideIcon();
                return;
            }
            ShowIcon();
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell.GetType() != typeof(ItemCell)) return;
            ItemCell itemCell = cell as ItemCell;
            print("Wrap items");
           
        }

        public void SetCount(int count)
        {
            this.count = count;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public Item GetItem()
        {
            return item;
        }
        public int GetCount()
        {
            return count;
        }
        public int GetIndex()
        {
            return index;
        }
    }
}