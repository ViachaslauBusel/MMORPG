using DataFileProtocol.Skills;
using Newtonsoft.Json;
using Skills.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SkillsRedactor
{
    public class SkillsExport
    {
        public static void Export(SkillsRegistry skillsList)
        {
          
               //SkillsContainer skillsContainer = new SkillsContainer();
               // foreach (SkillData skill in skillsList.skills)
               // {
               // DataFileProtocol.Skills.SkillData skillData = skill.skillType switch
               //     {
               //         SkillType.Melee => new MelleSkillData(),
               //         _ => new DataFileProtocol.Skills.SkillData(),
               //     };

               //     skillData.id = skill.id;
               //     skillData.skillType = (DataFileProtocol.Skills.SkillType)skill.skillType;
               //     skillData.branch = (DataFileProtocol.Skills.SkillBranch)skill.branch;
               //     skillData.applyingTime = (int)(skill.applyingTime * 1000);
               //     skillData.usingTime = (int)(skill._usingTime * 1000);
               //     skillData.reuseTime = (int)(skill.reuseTime * 1000);
               //     skillData.useAfter = skill.useAfter;
               //     skillData.animationId = (short)skill.animationType;

               //     if(skillData is MelleSkillData melleSkillData)
               //     {
               //         MelleSkill melleSkill = skill.GetMelleSkill();
               //         if(melleSkill == null)
               //         {
               //             Debug.LogError("Ошибка экспорта skills.dat - MelleSkill не найден");
               //             return;
               //         }

               //         melleSkillData.damage = (int)melleSkill.damage;
               //         melleSkillData.prickingDamage = (int)melleSkill.prickingDamage;
               //         melleSkillData.crushingDamage = (int)melleSkill.crushingDamage;
               //         melleSkillData.choppingDamage = (int)melleSkill.choppingDamage;
               //         melleSkillData.range = melleSkill.range;
               //         melleSkillData.angle = melleSkill.angle * Mathf.Deg2Rad;
               //         melleSkillData.staminaCost = melleSkill.stamina;
               //     }
               //     skillsContainer.Add(skillData);
               // }
                 
               // File.WriteAllText(@"Export/skills.dat", JsonConvert.SerializeObject(skillsContainer));
        }
    }
}