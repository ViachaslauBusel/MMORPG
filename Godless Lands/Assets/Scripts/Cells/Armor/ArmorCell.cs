using Cells.Armor;
using Items;
using Items.Data;
using Protocol.Data.Items;
using Protocol.MSG.Game.Equipment;
using RUCP;
using UnityEngine;
using UnityEngine.UI;
using ItemData = Items.ItemData;

namespace Cells
{
    public class ArmorCell : ItemCell
    {
        [SerializeField]
        private EquipmentType _cellType;
        private Texture2D _defaultIcon;

        public EquipmentType CellType => _cellType;

        private void Awake()
        {
            Init(ItemStorageType.Equipment, -1);
            _defaultIcon = _icon.sprite.texture;
            Hide();
        }

        public override void Use()
        {
            if (IsEmpty()) return;
            _itemUsageService.TakeFromEquipAndPutInBag(ItemStorageType.PrimaryBag, -1, _item.UniqueID);
        }

        public override void Hide()
        {
            if (_defaultIcon == null) return;
            _icon.sprite = Sprite.Create(_defaultIcon, new Rect(0.0f, 0.0f, _defaultIcon.width, _defaultIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        public override void Put(Cell cell)
        {

            if (cell.GetType() != typeof(ItemCell) || cell.IsEmpty())
            {
                return;//Если ячейка не для предметов или пустая
            }

            if (cell is not ItemCell itemCell || IsValidItemForCell(itemCell.GetItem()?.Data) == false)
            {
                return;//Если тип предмета не соответствует ячейке
            }
            //_cellStateCacheSystem.ShowIfUIDUquals(cell, cell.GetItemUID(), 500);
            cell.Use();//Использовать предмет
        }

        private bool IsValidItemForCell(ItemData itemData)
        {
           return itemData is EquipmentItemData equipmentItemData && equipmentItemData.EquipmentType == _cellType;
        }


        public override void Show()
        {
            if(_item != null)
                UpdateIcon();
        }
    }
}