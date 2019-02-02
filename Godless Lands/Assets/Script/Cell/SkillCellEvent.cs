using SkillsRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class SkillCellEvent : CellEvent
    {
        private GameObject prefabInformer; //Префаб бьекта c текстом информации о обьекте в ячейке



        private new void Start()
        {
            doubleClick = false;
            prefabInformer = Resources.Load<GameObject>("Cell/SkillInformer");
            base.Start();
        }

        public override void ShowInfo()
        {
            if (cell.IsEmpty()) return;
            SkillCell skillCell = cell as SkillCell;
            if (skillCell == null) return;
            Skill skill = skillCell.GetSkill();

            informer = Instantiate(prefabInformer);
            SkillInformer _informer = informer.GetComponent<SkillInformer>();
            _informer.Initial(Input.mousePosition, cellParent.parent);


            _informer.SetIcon(skillCell.GetIcon());
            _informer.SetName(skill.name);
            _informer.SetApplyTime(skill.applyingTime);
            _informer.SetReuseTime(skill.reuseTime);

            MelleSkill melleSkill = skill.GetMelleSkill();
            if(melleSkill != null)
            {
                _informer.SetRange(melleSkill.range);
                _informer.SetAngle(melleSkill.angle);
                _informer.SetStamina(melleSkill.stamina);
            }
            //  _informer.SetRange(skill.range);

            //  _informer.SetVitality(skill.viltality);
            _informer.setDescription(skill.description);
        }
    }
}