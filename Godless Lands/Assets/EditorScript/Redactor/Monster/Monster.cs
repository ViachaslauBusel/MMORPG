﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace MonsterRedactor
{
    [System.Serializable]
    public class Monster
    {
        public int id;
        public string prefabPath;
        public string name;
        public int hp;



        public GameObject prefab
        {
            get { return Resources.Load<GameObject>(prefabPath); }
#if UNITY_EDITOR
            set {
                if (value == null || !AssetDatabase.Contains(value)) return;
                prefabPath = AssetDatabase.GetAssetPath(value).Replace("Assets/Resources/","");
 
                prefabPath = prefabPath.Replace(".prefab", "");
                } 
#endif
        }
    }
}