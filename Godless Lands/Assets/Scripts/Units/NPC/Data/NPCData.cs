﻿
using ObjectRegistryEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NPCs
{
    [System.Serializable]
    public class NPCData : ScriptableObject, IDataObject
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