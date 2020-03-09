using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
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
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.TakeOffArmor);
            nw.write((int)GetItem().type);
            NetworkManager.Send(nw);
        }

        public override void HideIcon()
        {
            icon.sprite = Sprite.Create(defaultIcon, new Rect(0.0f, 0.0f, defaultIcon.width, defaultIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public override void Put(Cell cell)
        {

            if (cell.GetType() != typeof(ItemCell) || cell.IsEmpty()) return;//Если ячейка не для предметов или пустая

            ItemCell itemCell = cell as ItemCell;
            if (itemCell.GetItem().type != use) return;//Если тип предмета не соответствует ячейке
            cell.Use();//Использовать предмет
        }

        public override void ShowIcon()
        {
            if(item != null)
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}