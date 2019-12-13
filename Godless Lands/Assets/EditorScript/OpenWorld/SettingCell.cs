#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class SettingCell
    {
        private ScriptableObject cell;
        private ScriptableObject dontSaveCell;
        private string key;

        public SettingCell(string key)
        {
            this.key = key;
            string path = PlayerPrefs.GetString(key);
            cell = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            dontSaveCell = cell;
        }
        public void SavePrefab()
        {
            cell = dontSaveCell;
            string pathToPrefab = AssetDatabase.GetAssetPath(cell);
            PlayerPrefs.SetString(key, pathToPrefab);
        }
        public ScriptableObject Save { get { return cell; } set { cell = value; } }
        public ScriptableObject DontSave { get { return dontSaveCell; } set { dontSaveCell = value; } }
        public bool IsDontSave()
        {
            return cell != dontSaveCell;
        }
    }
}
#endif