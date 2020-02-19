#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MonsterRedactor
{

    public class CreateWorldMonstersAsset
    {
        [MenuItem("Assets/Create/WorldMonsterList")]
        public static void CreateAsset()
        {
            CreateAssetWorldMonster();
        }

        public static void CreateAssetWorldMonster()
        {
            WorldMonstersList asset = ScriptableObject.CreateInstance<WorldMonstersList>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";

            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New World Monsters.asset");

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