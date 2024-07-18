using Animation;
using ObjectRegistryEditor;
using UnityEngine;

namespace Skills.Data
{
    [System.Serializable]
    public class SkillData : ScriptableObject, IDataObject
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private Texture2D _icon;
        [SerializeField]
        private string _name;
        [SerializeField]
        private SkillBranch _branch;
        [SerializeField]
        private float _applyingTime;
        [SerializeField]
        private float _usingTime;
        [SerializeField]
        private float _reuseTime;
        [SerializeField]
        private bool _useAfter;
        [SerializeField]
        private AnimationType _animationType;
        [SerializeField]
        private string _description;


        public int ID => _id;
        public string Name => _name;
        public Texture Preview => _icon;
        public Texture2D Icon => _icon;
        public SkillBranch Branch => _branch;
        public float ApplyingTime => _applyingTime;
        public float UsingTime => _usingTime;
        public float ReuseTime => _reuseTime;
        public bool UseAfter => _useAfter;
        public AnimationType AnimationType => _animationType;
        public string Description => _description;


        public void Initialize(int id)
        {
            _id = id;
        }
    }
}