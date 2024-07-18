using Skills.Data;
using SkillsRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class SkillCellEvent : CellEvent
    {
        private static GameObject prefabInformer; //Префаб бьекта c текстом информации о обьекте в ячейке



        private new void Start()
        {
            doubleClick = false;
            if (prefabInformer == null)
            { prefabInformer = Resources.Load<GameObject>("Cell/SkillInformer"); }
            base.Start();
        }

        public override void ShowInfo()
        {
            if (cell.IsEmpty()) return;
            SkillCell skillCell = cell as SkillCell;
            if (skillCell == null) return;
            SkillData skill = skillCell.GetSkill();

            informer = SkillInfo(skill, cellParent.parent);
        }

        public static GameObject SkillInfo(SkillData skill, Transform parent)
        {
            GameObject obj = Instantiate(prefabInformer);
            SkillInformer _informer = obj.GetComponent<SkillInformer>();
            _informer.Initial(Input.mousePosition, parent);


            _informer.SetIcon(skill.Icon);
            _informer.SetName(skill.name);
            _informer.SetApplyTime(skill.UsingTime);
            _informer.SetReuseTime(skill.ReuseTime);

            if (skill is MelleSkillData melleSkill)
            {
                _informer.SetRange(melleSkill.Range);
                _informer.SetAngle(melleSkill.Angle);
                _informer.SetStamina(melleSkill.Stamina);
            }
            //  _informer.SetRange(skill.range);

            //  _informer.SetVitality(skill.viltality);
            _informer.setDescription(skill.Description);

            return obj;
        }
    }
}