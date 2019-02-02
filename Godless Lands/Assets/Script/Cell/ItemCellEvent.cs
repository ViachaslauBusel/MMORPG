using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class ItemCellEvent : CellEvent
    {
        private GameObject prefabInformer; //Префаб бьекта c текстом информации о обьекте в ячейке
      
       

        private new void Start()
        {
            prefabInformer = Resources.Load<GameObject>("Cell/PanelItemInfo");
            base.Start();
        }

        public override void ShowInfo()
        {
            if (cell.IsEmpty()) return;
            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null) return;
            Item item = itemCell.GetItem();

            informer = Instantiate(prefabInformer);
             ItemInformer _informer = informer.GetComponent<ItemInformer>();
            _informer.Initial(Input.mousePosition, cellParent.parent);

           
            _informer.SetIcon(itemCell.GetIcon());
            _informer.SetName(item.nameItem);
            if (item.stack) _informer.SetCount(itemCell.GetCount());
            switch (item.use)
            {
                case ItemUse.Weapon:
                    WeaponItem weapon = item.serializableObj as WeaponItem;
                    if (weapon == null) break;
                    _informer.SetAtack(weapon.physicalAttack);
                    _informer.SetSpeedAtack(weapon.speed);
                    if (weapon.prickingDamage > 0) _informer.SetPrickingDamage(weapon.prickingDamage);
                    if (weapon.crushingDamage > 0) _informer.SetCrushingDamage(weapon.crushingDamage);
                    if (weapon.choppingDamage > 0) _informer.SetChoppingDamage(weapon.choppingDamage);
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
        }
    }
}
