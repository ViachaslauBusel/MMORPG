#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Resource
{
    public class CreateResourceAsset
    {
        [MenuItem("Assets/Create/ResourcesList")]
        public static void Create()
        {
            CreateAsset.Create<ResourceList>("ResourcesList");
        }

        [MenuItem("Assets/Create/WorldResourcesList")]
        public static void CreateWorld()
        {
            CreateAsset.Create<WorldResourcesList>("WorldResourcesList");
        }
    }
}
#endif