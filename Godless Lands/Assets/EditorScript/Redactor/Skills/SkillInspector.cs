#if UNITY_EDITOR
using Animation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
namespace SkillsRedactor
{
    [CustomEditor(typeof(SkillEditor))]
    [CanEditMultipleObjects]
    public class SkillInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            SkillEditor skillEditor = (SkillEditor)target;
            Skill skill = skillEditor.skill;

            GUILayout.Space(15.0f);

            GUILayout.Label("ID: " + skill.id);

            GUILayout.Space(10.0f);

            skill.icon = EditorGUILayout.ObjectField("Icon", skill.icon, typeof(Texture2D), false) as Texture2D;

            skill.name = EditorGUILayout.TextField("name:", skill.name);
            skill.branch = (SkillBranch)EditorGUILayout.EnumPopup("Branch: ", skill.branch);
            skill.applyingTime = EditorGUILayout.FloatField("Use Time: ", skill.applyingTime);
            skill.reuseTime = EditorGUILayout.FloatField("Reuse Time: ", skill.reuseTime);
            skill.useAfter = EditorGUILayout.Toggle("Use after?: ", skill.useAfter);
            skill.animationType = (AnimationType)EditorGUILayout.EnumPopup("Animation use: ", skill.animationType);
            skill.description = EditorGUILayout.TextField("Description", skill.description);

            skill.skillType = (SkillType)EditorGUILayout.EnumPopup("Skill Type: ", skill.skillType);



            if (skillEditor.serializableObject == null)
            {
                skillEditor.serializableObject = skill.GetObject();
                if(skillEditor.serializableObject == null)
                {
                    switch (skill.skillType)
                    {
                        case SkillType.None:
                            skillEditor.serializableObject = null;
                            skill.serializableObj = null;
                            return;
                        case SkillType.Melee:
                            skillEditor.serializableObject = new MelleSkill();
                            break;
                        default: return;
                    }
                }
            }

            if (skillEditor.serializableObject.GetType() == typeof(MelleSkill))
            {
                if (skill.skillType != SkillType.Melee) { skill.serializableObj = null; skillEditor.serializableObject = null; return; }
                MelleSkill melleSkill = skillEditor.serializableObject as MelleSkill;

                melleSkill.damage = EditorGUILayout.FloatField("Damage: ", melleSkill.damage);
                melleSkill.prickingDamage = EditorGUILayout.FloatField("Pricking: ", melleSkill.prickingDamage);
                melleSkill.crushingDamage = EditorGUILayout.FloatField("Crushing: ", melleSkill.crushingDamage);
                melleSkill.choppingDamage = EditorGUILayout.FloatField("Chopping: ", melleSkill.choppingDamage);
                melleSkill.range = EditorGUILayout.FloatField("Range: ", melleSkill.range);
                melleSkill.angle = EditorGUILayout.Slider("Angle: ", melleSkill.angle, 0.0f, 180.0f);
                melleSkill.stamina = EditorGUILayout.IntField("Stamina: ", melleSkill.stamina);
            }

                if (GUILayout.Button("Save"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    MemoryStream stream = new MemoryStream();
                    formatter.Serialize(stream, skillEditor.serializableObject);
                    skill.serializableObj = stream.ToArray();
                }
            
        }
    }
}
#endif