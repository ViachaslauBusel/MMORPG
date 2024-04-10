using Animation;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SkillsRedactor
{
    [System.Serializable]
    public class Skill {

        public int id;
        public Texture2D icon;
        public string name;
        public SkillBranch branch;
        public float applyingTime;
        public float _usingTime;
        public float reuseTime;
        public bool useAfter;
        public AnimationType animationType;
        public string description;
        public SkillType skillType;
        public byte[] serializableObj;
 
        public MelleSkill GetMelleSkill()
        {
            if (serializableObj.Length == 0) return null;
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(serializableObj);
            return formatter.Deserialize(stream) as MelleSkill;
        }

        public System.Object GetObject()
        {
            try
            {
                if (serializableObj.Length == 0) return null;
                Stream stream = new MemoryStream(serializableObj);
                return new BinaryFormatter().Deserialize(stream);
            }
            catch
            {
                serializableObj = null;
                return null;
            }
        }

    }
}