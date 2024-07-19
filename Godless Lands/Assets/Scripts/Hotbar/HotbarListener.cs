using Cells;
using Equipment;
using Inventory;
using Network.Core;
using Protocol;
using Protocol.MSG.Game.Hotbar;
using RUCP;
using Skills;
using UnityEngine;
using Zenject;

namespace Hotbar
{
    public class HotbarListener : MonoBehaviour
    {
        private static BarCell[] _barCells;
        private NetworkManager _networkManager;
        private PlayerSkillsHolder _playerSkillsHolder;
        private InventoryModel _inventoryModel;
        private EquipmentModel _equipmentModel;

        [Inject]
        private void Construct(NetworkManager networkManager, PlayerSkillsHolder playerSkillsHolder, InventoryModel inventoryModel, EquipmentModel equipmentModel)
        {
            _networkManager = networkManager;
            _playerSkillsHolder = playerSkillsHolder;
            _inventoryModel = inventoryModel;
            _equipmentModel = equipmentModel;
        }

        private void Awake()
        {
            _barCells = GetComponentsInChildren<BarCell>();
            for(int i=0; i < _barCells.Length; i++)
            {
                _barCells[i].SetIndex(i);
            }

            _networkManager.RegisterHandler(Opcode.MSG_HOTBAR_UPDATE, UpdateBarCell);
            _playerSkillsHolder.OnSkillsUpdate += RedrawCells;
            _inventoryModel.OnInventoryUpdate += RedrawCells;
            _equipmentModel.OnItemsChanged += RedrawCells;
        }

        private void RedrawCells()
        {
            foreach (BarCell cell in _barCells)
            {
                cell.Redraw();
            }
        }

        private void UpdateBarCell(Packet packet)
        {
            packet.Read(out MSG_HOTBAR_UPDATE_SC update);

            foreach (var cellUpdate in update.Cells)
            {
                BarCell cell = _barCells[cellUpdate.CellIndex];

                if (cell == null)
                {
                    Debug.LogError($"Hotbar cell with index {cellUpdate.CellIndex} not found");
                    continue;
                }

                CellContentInfo cellContent = new CellContentInfo(cellUpdate.CellType, cellUpdate.CellValue);
                cell.SetContent(cellContent);
            }

            RedrawCells();
        }
         
        public static void Hide(float time)
        {
            foreach(BarCell cell in _barCells)
            {
                cell.Hide(time);
            }
        }

        private void OnDestroy()
        {
            _networkManager?.UnregisterHandler(Opcode.MSG_HOTBAR_UPDATE);
            _playerSkillsHolder.OnSkillsUpdate -= RedrawCells;
            _inventoryModel.OnInventoryUpdate -= RedrawCells;
            _equipmentModel.OnItemsChanged -= RedrawCells;
        }
    }
}