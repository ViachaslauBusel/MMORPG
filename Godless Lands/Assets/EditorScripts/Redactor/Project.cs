#if UNITY_EDITOR
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Project
{
   // private static ItemsList _itemsList;

    public static ItemsRegistry ItemsList
    {
        get {
         //   if(_itemsList == null)
       //     {
                string path = PlayerPrefs.GetString("settingItemsList");
            //   _itemsList =
            //   }
            return AssetDatabase.LoadAssetAtPath<ItemsRegistry>(path);
        }
        set
        {
           // _itemsList = value;
            string path = AssetDatabase.GetAssetPath(value);
            PlayerPrefs.SetString("settingItemsList", path);
        }
    }
}
#endif