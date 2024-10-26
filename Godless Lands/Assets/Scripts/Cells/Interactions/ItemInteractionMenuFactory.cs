using Items;
using Items.Data;
using UnityEngine;
using Zenject;

namespace Cells.Interactions
{
    public class ItemInteractionMenuFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dragCellPrefab;
        [SerializeField]
        private GameObject _infoPanelPrefab;
        [SerializeField]
        private GameObject _menuPrefab;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public DragCell CreateDragCell(Transform transform, Cell cell, Vector3 postionClick)
        {
            Debug.Log("CreateDragCell");
            DragCell dragCell = _diContainer.InstantiatePrefab(_dragCellPrefab).GetComponent<DragCell>();
            dragCell.CaptureItem(transform, cell, postionClick);
            return dragCell;
        }

        public GameObject CreateItemInfoPanel(Transform parent, Item item)
        {
            GameObject obj = _diContainer.InstantiatePrefab(_infoPanelPrefab);
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

        internal void CreateContextMenuForItem(ItemCell itemCell, CellParent cellParent)
        {
            CellMenu menu = _diContainer.InstantiatePrefabForComponent<CellMenu>(_menuPrefab);
            menu.Initial(cellParent.parent);
            menu.use.onClick.AddListener(itemCell.Use);
            menu.move.onClick.AddListener(itemCell.TransferItemToAnotherBag);
        }
    }
}
