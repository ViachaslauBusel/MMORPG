using Factories;
using ObjectRegistryEditor;
using Protocol.Data.Items;
using Protocol.Data.Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Units.Resource.Data
{
    public class ResourceHarvestData : ScriptableObject, IDataObject, IPrefabHolder
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private AssetReferenceT<GameObject> _prefab;
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
        public IReadOnlyCollection<ItemDrop> Drops => _drops;
        public ProfessionEnum Profession => _profession;
        public int Exp => _exp;
        public int Stamina => _stamina;
        public AssetReferenceT<GameObject> Prefab => _prefab;

        public void Initialize(int id)
        {
            _id = id;
        }

        public ResourceInfo ToServerData()
        {
            return new ResourceInfo
            {
                ID = _id,
                Profesion = (int)_profession,
                Exp = _exp,
                Stamina = _stamina,
                Drops = _drops.Select(d => new DropItemData()
                {
                    ItemID = d.ID,
                    MinAmount = d.MinAmount,
                    MaxAmount = d.MaxAmount,
                    Chance = d.Chance
                }).ToList()
            };
        }
    }
}
