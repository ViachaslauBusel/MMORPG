using Data;
using ObjectRegistryEditor;
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

        public int ID => _id;
        public string Name => _name;
#if UNITY_EDITOR
        public Texture Preview => AssetPreview.GetAssetPreview(_prefab.editorAsset);
#else
        public Texture Preview => null;
#endif
        public AssetReferenceT<GameObject> Prefab => _prefab;


        public void Initialize(int id)
        {
            _id = id;
        }
    }
}
