using Items;
using RUCP;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{


    public class ItemCell : Cell
    {
        protected Item item;
        protected int index;
     //   protected int objectID;
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

        /// <summary>
        /// использовать предмет
        /// </summary>
        public override void Use()
        {
            if (IsEmpty()) return;
            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.UseItem);
            //nw.WriteInt(item.objectID);
            //nw.WriteInt(item.id);
            //NetworkManager.Send(nw);
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
        /// Положить предмет в ячейку
        /// </summary>
        /// <param name="item"></param>
        public virtual void PutItem(Item item)
        {
           
            this.item = item;
            if (IsEmpty() || !item.IsExist())//Если предмет не существует
            {
                if (countTxt != null)
                    countTxt.text = "";
                HideIcon();
                return;
            }
           // item.count = count;
            ShowIcon();
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            if (countTxt != null)
            {
                if (item.stack)
                    countTxt.text = item.count.ToString();
                else countTxt.text = "";
            }
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
        public override int GetObjectID()
        {
            if (IsEmpty()) return 0;
            return item.objectID;
        }

        public override string GetText()
        {
            if(!IsEmpty() && item.stack) return GetCount().ToString();
            return base.GetText();
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