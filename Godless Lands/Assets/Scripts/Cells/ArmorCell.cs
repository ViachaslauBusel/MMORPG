using Items;
using Protocol.Data.Items;
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

        private void Awake()
        {
            Init(ItemStorageType.Equipment, -1);
            defaultIcon = icon.sprite.texture;
            Hide();
        }

        public override void Use()
        {
            if (IsEmpty()) return;
            _itemUsageService.TakeFromEquipAndPutInBag(ItemStorageType.PrimaryBag, -1, _item.UniqueID);
        }

        public override void Hide()
        {
            if(defaultIcon == null) return;
            icon.sprite = Sprite.Create(defaultIcon, new Rect(0.0f, 0.0f, defaultIcon.width, defaultIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public override void Put(Cell cell)
        {

            if (cell.GetType() != typeof(ItemCell) || cell.IsEmpty())
            {
                return;//Если ячейка не для предметов или пустая
            }

            ItemCell itemCell = cell as ItemCell;
            if (itemCell.GetItem().Data.type != use)
            {
                return;//Если тип предмета не соответствует ячейке
            }
            //_cellStateCacheSystem.ShowIfUIDUquals(cell, cell.GetItemUID(), 500);
            cell.Use();//Использовать предмет
        }

        public override void Show()
        {
            if(_item != null)
                UpdateIcon();
        }
    }
}