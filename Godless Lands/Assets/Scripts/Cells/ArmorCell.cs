using Items;
using RUCP;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class ArmorCell : ItemCell
    {
        private Texture2D defaultIcon;
        //Как используется ячейка
        public ItemType use;

        private new void Awake()
        {
            icon = transform.Find("Icon").GetComponent<Image>();
            defaultIcon = icon.sprite.texture;
        }

        public override void Use()
        {
            if (IsEmpty()) return;
            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.TakeOffArmor);
            //nw.WriteInt((int)GetItem().type);
            //NetworkManager.Send(nw);
        }

        public override void HideIcon()
        {
            icon.sprite = Sprite.Create(defaultIcon, new Rect(0.0f, 0.0f, defaultIcon.width, defaultIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public override void Put(Cell cell)
        {

            if (cell.GetType() != typeof(ItemCell) || cell.IsEmpty()) return;//Если ячейка не для предметов или пустая

            ItemCell itemCell = cell as ItemCell;
            if (itemCell.GetItem().Data.type != use) return;//Если тип предмета не соответствует ячейке
            cell.Use();//Использовать предмет
        }

        public override void ShowIcon()
        {
            if(_item != null)
                UpdateIcon();
        }
    }
}