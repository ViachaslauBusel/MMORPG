using Items;
using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Units.Resource.Data
{
    public class ResourceHarvestData : ScriptableObject, IDataObject
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private AssetReferenceT<GameObject> _prefab;
        [SerializeField]
        private float _startSpawn;
        [SerializeField]
        private float _timeSpawn;
        [SerializeField]
        private List<ItemDrop> _drops;
        [SerializeField]
        private ProfessionEnum _profession; // Profession that gains experience
        [SerializeField]
        private int _exp; // Experience points gained
        [SerializeField]
        private int _stamina; // Stamina cost

        public int ID => _id;
        public string Name => _name;
#if UNITY_EDITOR
        public Texture Preview => AssetPreview.GetAssetPreview(_prefab.editorAsset);
#else
        public Texture Preview => null;
#endif
        public float StartSpawn => _startSpawn;
        public float TimeSpawn => TimeSpawn;
        public IReadOnlyCollection<ItemDrop> Drops => _drops;
        public ProfessionEnum Profession => _profession;
        public int Exp => _exp;
        public int Stamina => _stamina;
        public AssetReferenceT<GameObject> Prefab => _prefab;

        public void Initialize(int id)
        {
            _id = id;
        }
    }
}
