using Factories;
using ObjectRegistryEditor;
using Protocol.Data.Units.Monsters;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Units.Monster
{
    public class MonsterData : ScriptableObject, IDataObject, IPrefabHolder
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private AssetReferenceT<GameObject> _prefab;
        [SerializeField]
        private int _health;

        public int ID => _id;
        public string Name => _name;
#if UNITY_EDITOR
        public Texture Preview => AssetPreview.GetAssetPreview(_prefab.editorAsset);
#else
        public Texture Preview => null;
#endif
        public AssetReferenceT<GameObject> Prefab => _prefab;
        public int Health => _health;


        public void Initialize(int id)
        {
            _id = id;
        }

        public MonsterInfo ToServerData()
        {
            return new MonsterInfo
            {
                ID = ID,
                HP = Health
            };
        }
    }
}
