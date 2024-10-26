using Dialogues.Data.Nodes;
using Factories;
using ObjectRegistryEditor;
using Protocol.Data.Units.NPCs;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NPCs
{
    [System.Serializable]
    public class NPCData : ScriptableObject, IDataObject, IPrefabHolder
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private AssetReferenceT<GameObject> _prefab;
        [SerializeField]
        private DialogData _dialog;

        public int ID => _id;
        public string Name => _name;
#if UNITY_EDITOR
        public Texture Preview => AssetPreview.GetAssetPreview(_prefab.editorAsset);
#else
        public Texture Preview => null;
#endif
        public AssetReferenceT<GameObject> Prefab => _prefab;
        public DialogData Dialog => _dialog;


        public void Initialize(int id)
        {
            _id = id;
        }

        public NpcSData ToServerData()
        {
            return new NpcSData(_id,
                                _dialog.Nodes.OfType<StoreNode>()
                                             .Select(n => n.ShopData.ToServerData())
                                             .FirstOrDefault()
                                );
        }
    }
}
