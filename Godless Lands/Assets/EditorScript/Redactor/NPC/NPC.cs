using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCRedactor
{
    [System.Serializable]
    public class NPC
    {
        public int id;
        public string prefabPath;
        public string name;



        public GameObject prefab
        {
            get { return Resources.Load<GameObject>(prefabPath); }
#if UNITY_EDITOR
            set
            {
                if (value == null || !AssetDatabase.Contains(value)) return;
                prefabPath = AssetDatabase.GetAssetPath(value).Replace("Assets/Resources/", "");

                prefabPath = prefabPath.Replace(".prefab", "");
            }
#endif
        }
    }
}