#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace NPCRedactor
{
    public class CreateWorldNPCAsset
    {
        [MenuItem("Assets/Create/WorldNPCList")]
        public static void CreateAsset()
        {
            CreateAssetWorldMonster();
        }

        public static void CreateAssetWorldMonster()
        {
            WorldNPCList asset = ScriptableObject.CreateInstance<WorldNPCList>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";

            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New World NPCs.asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            //  EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}
#endif