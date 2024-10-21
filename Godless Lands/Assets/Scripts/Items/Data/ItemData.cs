using ObjectRegistryEditor;
using Protocol.Data.Items.Types;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Items
{
    [System.Serializable]
    public class ItemData : ScriptableObject, IDataObject
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private Texture2D _icon;
        [SerializeField]
        private string _description;
        [SerializeField]
        private bool _isStackable;
        [SerializeField]
        private int _weight;
        [SerializeField]
        private int _price;
        [SerializeField]
        private AssetReference _prefab;

        public int ID => _id;
        public string Name => _name;

#if UNITY_EDITOR
        public Texture Preview => _icon;
#else
        public Texture Preview => null;
#endif
        public Texture2D Icon => _icon;
        public string Description => _description;
        public bool IsStackable => _isStackable;
        public int Weight => _weight;
        public int Price => _price;
        public AssetReference Prefab => _prefab;

        public void Initialize(int id)
        {
            _id = id;
        }

        public virtual ItemSData ToServerData()
        {
            return new ItemSData(ID, IsStackable, Weight, Price);
        }
    }

    
}