using Factories;
using ObjectRegistryEditor;
using Protocol.Data.Units.Monsters;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField]
        private List<ItemDrop> _drop;

        public int ID => _id;
        public string Name => _name;
#if UNITY_EDITOR
        public Texture Preview => AssetPreview.GetAssetPreview(_prefab.editorAsset);
#else
        public Texture Preview => null;
#endif
        public AssetReferenceT<GameObject> Prefab => _prefab;
        public int Health => _health;
        public IReadOnlyCollection<ItemDrop> Drop => _drop;


        public void Initialize(int id)
        {
            _id = id;
        }

        public MonsterSData ToServerData()
        {
            return new MonsterSData
            (
                ID,
                Health,
                _drop.Select(d => d.ToServerData()).ToList()
            );
        }
    }
}
