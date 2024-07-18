using UnityEngine;

namespace Skills.Data
{
    public class MelleSkillData : SkillData
    {
        [Header("Melee Skill Data")]
        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _range;
        [SerializeField]
        private int _angle;
        [SerializeField]
        private int _stamina;


        public int Damage => _damage;
        public int Range => _range;
        public int Angle => _angle;
        public int Stamina => _stamina;
    }
}
