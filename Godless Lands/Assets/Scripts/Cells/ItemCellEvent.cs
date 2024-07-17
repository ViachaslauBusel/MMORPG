using Items;
using Items.Data;
using UnityEngine;

namespace Cells
{
    public class ItemCellEvent : CellEvent
    {
        private static GameObject prefabInformer; //Префаб бьекта c текстом информации о обьекте в ячейке
        private static GameObject prefabMenu;
        private GameObject Objmenu;


        private new void Start()
        {
            if (prefabInformer == null)
            {
                prefabInformer = Resources.Load<GameObject>("Cell/PanelItemInfo");
            }
            if (prefabMenu == null)
                prefabMenu = Resources.Load<GameObject>("Cell/Menu");
            base.Start();
        }

        public override void RightClick()//Использвоние предмета
        {
            if (cell.GetType() == typeof(ItemCell) && !cell.IsEmpty())
            {
                ItemCell itemCell = cell as ItemCell;
                Objmenu = Instantiate(prefabMenu);
                CellMenu menu = Objmenu.GetComponent<CellMenu>();
                menu.Initial(cellParent.parent);
                menu.use.onClick.AddListener(itemCell.Use);
                menu.move.onClick.AddListener(itemCell.TransferItemToAnotherBag);
                //   Button use =
            }
            else cell.Use();
        }


        public override void ShowInfo()
        {
            if (cell.IsEmpty()) return;
            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null) return;

            informer = ItemInfo(cellParent.parent, itemCell.GetItem());
        }

        public static GameObject ItemInfo(Transform parent, Item item)
        {
            GameObject obj = Instantiate(prefabInformer);
            ItemInformer _informer = obj.GetComponent<ItemInformer>();
            _informer.Initial(Input.mousePosition, parent);


            _informer.SetIcon(item.Data.Icon);

            if (item.Data is EquipmentItemData)
                _informer.SetName(item.EnchantLevel, item.Data.Name, item.Count, item.Durability);
            else _informer.SetName(item.Data.Name);

            if (item.Data.IsStackable) _informer.SetCount(item.Count);
            switch (item.Data)
            {
                case WeaponItemData weapon:
                    _informer.SetLevel(weapon.WeaponType);
                    _informer.SetSpeedAtack(weapon.SpeedAttack);
                    _informer.SetAtack(weapon.MinDamege, weapon.MaxDamage, item.Count, item.EnchantLevel);
                    _informer.SetDurability(item.Durability, item.MaxDurability);
                
                //    if (weapon.prickingDamage > 0) _informer.SetPrickingDamage(weapon.prickingDamage, count);
                //    if (weapon.crushingDamage > 0) _informer.SetCrushingDamage(weapon.crushingDamage, count);
                 //   if (weapon.choppingDamage > 0) _informer.SetChoppingDamage(weapon.choppingDamage, count);
                    break;
                case ElixirItemData elixir:
                    if (elixir.HpRestore > 0) _informer.SetHP(elixir.HpRestore);
                    if (elixir.MpRestore > 0) _informer.SetMP(elixir.MpRestore);
                    if (elixir.StaminaRestore > 0) _informer.SetStamina(elixir.StaminaRestore);
                    break;
            }
            _informer.setDescription(item.Data.Description);
            _informer.setWeight(item.Data.Weight);
            return obj;
        }
    }
}
