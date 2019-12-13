
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCs
{
    [System.Serializable]
    public class NPCPrefab
    {
        public int id;
        public string prefabPath;
        public string name;



        public GameObject prefab
        {
            get { return Resources.Load<GameObject>(prefabPath); }

            set
            {
                if (value == null || !AssetDatabase.Contains(value)) return;
                prefabPath = AssetDatabase.GetAssetPath(value).Replace("Assets/Resources/", "");

                prefabPath = prefabPath.Replace(".prefab", "");
            }

        }
    }
}
