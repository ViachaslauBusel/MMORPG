using Equipment;
using Inventory;
using Items;
using Skills;
using Skills.Data;
using UnityEngine;

namespace Cells
{
    public class CellContentToRenderConverter
    {
        private PlayerSkillsHolder _playerSkillsHolder;
        private InventoryModel _inventoryModel;
        private EquipmentModel _equipmentModel;

        
        public CellContentToRenderConverter(PlayerSkillsHolder playerSkillsHolder, InventoryModel inventoryModel, EquipmentModel equipmentModel)
        {
            _playerSkillsHolder = playerSkillsHolder;
            _inventoryModel = inventoryModel;
            _equipmentModel = equipmentModel;
        }

        public CellRenderInfo Convert(CellContentInfo cellContentInfo)
        {
            if(cellContentInfo == null)
            {
                return null;
            }

           CellRenderInfo cellRenderInfo = cellContentInfo.Type switch
           {
               CellContentType.Item => FromItemUID(cellContentInfo.ID),
               CellContentType.Skill => FromSkillID(cellContentInfo.ID),
               _ => null
           };

            return cellRenderInfo;
        }

        private CellRenderInfo FromItemUID(int itemUID)
        {
            Item item = _inventoryModel.FindItem(itemUID);
            item ??= _equipmentModel.FindItem(itemUID);

            if (item == null)
            {
                Debug.LogWarning($"CellContentToRenderConverter: Item with id {itemUID} not found");
                return null;
            }

            return CellRenderInfo.CreateByItem(item);
        }

        private CellRenderInfo FromSkillID(int skillID)
        {
            SkillData skill = _playerSkillsHolder.GetSkill(skillID);

            if (skill == null)
            {
                Debug.LogWarning($"CellContentToRenderConverter: Skill with id {skillID} not found");
                return null;
            }

            return CellRenderInfo.CreateBySkill(skill);
        }
    }
}
