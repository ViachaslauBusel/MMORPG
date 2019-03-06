using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class ItemCellEvent : CellEvent
    {
        private static GameObject prefabInformer; //Префаб бьекта c текстом информации о обьекте в ячейке
      
       

        private new void Start()
        {
            if (prefabInformer == null)
            {
                prefabInformer = Resources.Load<GameObject>("Cell/PanelItemInfo");
            }
            base.Start();
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


            _informer.SetIcon(item.texture);

            if (item.use == ItemUse.Weapon || item.use == ItemUse.Armor)
                _informer.SetName(item.enchant_level, item.nameItem, item.count, item.durability);
            else _informer.SetName(item.nameItem);

            if (item.stack) _informer.SetCount(item.count);
            switch (item.use)
            {
                case ItemUse.Weapon:
                    WeaponItem weapon = item.serializableObj as WeaponItem;
                    if (weapon == null) break;
                    _informer.SetLevel(weapon.weaponType);
                    _informer.SetSpeedAtack(weapon.speed);
                    _informer.SetAtack(weapon.minDamege, weapon.maxDamage, item.count, item.enchant_level);
                    _informer.SetDurability(item.durability, item.maxDurability);
                
                //    if (weapon.prickingDamage > 0) _informer.SetPrickingDamage(weapon.prickingDamage, count);
                //    if (weapon.crushingDamage > 0) _informer.SetCrushingDamage(weapon.crushingDamage, count);
                 //   if (weapon.choppingDamage > 0) _informer.SetChoppingDamage(weapon.choppingDamage, count);
                    break;
                case ItemUse.RestorePoints:
                    RestorePointsItem restorePoints = item.serializableObj as RestorePointsItem;
                    if (restorePoints == null) break;
                    if (restorePoints.hp > 0) _informer.SetHP(restorePoints.hp);
                    if (restorePoints.mp > 0) _informer.SetMP(restorePoints.mp);
                    if (restorePoints.stamina > 0) _informer.SetStamina(restorePoints.stamina);
                    break;
            }
            _informer.setDescription(item.description);
            _informer.setWeight(item.weight);
            return obj;
        }
    }
}
