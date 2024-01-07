using Skills;
using SkillsRedactor;
using UnityEngine;
using Zenject;

namespace Cells
{
    public class CellContentToRenderConverter : MonoBehaviour
    {
       
        private PlayerSkillsHolder _playerSkillsHolder;

        [Inject]
        private void Construct(PlayerSkillsHolder playerSkillsHolder)
        {
            _playerSkillsHolder = playerSkillsHolder;
        }

        public CellRenderInfo Convert(CellContentInfo cellContentInfo)
        {
            if(cellContentInfo == null)
            {
                return null;
            }

           CellRenderInfo cellRenderInfo = cellContentInfo.Type switch
           {
               //CellContentType.Item => ConvertItem(cellContentInfo.ID),
               CellContentType.Skill => FromSkillID(cellContentInfo.ID),
               _ => null
           };

            return cellRenderInfo;

        }

        private CellRenderInfo FromSkillID(int skillID)
        {
            Skill skill = _playerSkillsHolder.GetSkill(skillID);

            if (skill == null)
            {
                Debug.LogError($"CellContentToRenderConverter: Skill with id {skillID} not found");
                return null;
            }

            return CellRenderInfo.CreateBySkill(skill);
        }
    }
}
