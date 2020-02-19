using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SkillsRedactor
{
    public class SkillsExport
    {
        public static void Export(SkillsList skillsList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/skills.dat", FileMode.Create)))
            {
                foreach (Skill skill in skillsList.skills)
                {
                    stream_out.Write((byte)skill.skillType);
                    stream_out.Write((byte)skill.branch);
                    stream_out.Write(skill.id);
                    stream_out.Write((int)(skill.applyingTime * 1000));
                    stream_out.Write((int)(skill.reuseTime * 1000));
                    stream_out.Write(skill.useAfter);
                    stream_out.Write((byte)skill.animationType);
                   

                    if(skill.skillType == SkillType.Melee)
                    {
                        MelleSkill melleSkill = skill.GetMelleSkill();
                        if(melleSkill == null)
                        {
                            Debug.LogError("Ошибка экспорта skills.dat - MelleSkill не найден");
                            return;
                        }
                        stream_out.Write(melleSkill.damage);
                        stream_out.Write(melleSkill.prickingDamage);
                        stream_out.Write(melleSkill.crushingDamage);
                        stream_out.Write(melleSkill.choppingDamage);
                        stream_out.Write(melleSkill.range);
                        stream_out.Write(melleSkill.angle * Mathf.Deg2Rad);
                        stream_out.Write(melleSkill.stamina);
                    }
                }
            }
        }
    }
}