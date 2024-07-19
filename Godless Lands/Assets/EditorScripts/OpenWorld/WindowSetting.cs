#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MonsterRedactor;
using Resource;
using Items;
using NPCRedactor;
using OpenWorld;
using NPCs;

namespace OpenWorldEditor
{
    public class WindowSetting
    {
       
        private static int _areaViseble = 1;
        private static int dontSaveAreaVisible = 1;
        private static SettingCell editMap;
        private static SettingCell _monstersList;
        private static SettingCell _worldMonstrList;
        private static SettingCell _NPCList;
        private static SettingCell _worldNPCList;
        private static SettingCell _resourceList;
        private static SettingCell _worldResourcesList;
        private static SettingCell _machineList;
        private static SettingCell _itemsList;

       
        public static int areaVisible { get { return _areaViseble; } }
        public static Map Map { get { return editMap.Save as Map; } }
        public static WorldMonstersList WorldMonsterList { get { return _worldMonstrList.Save as WorldMonstersList; } }
        public static NPCsRegistry NPCList { get { return _NPCList.Save as NPCsRegistry; } }
        public static WorldNPCList WorldNPCList { get { return _worldNPCList.Save as WorldNPCList; } }
        public static WorldResourcesList WorldResourcesList { get { return _worldResourcesList.Save as WorldResourcesList; } }
        public static MachineList MachineList { get { return _machineList.Save as MachineList; } }
        public static ItemsRegistry ItemsList { get { return _itemsList.Save as ItemsRegistry; } }

        private static Vector2 vSbarValue = Vector2.zero;

        public static void Draw()
        {
            vSbarValue = EditorGUILayout.BeginScrollView(vSbarValue, false, true);

            GUILayout.Space(15.0f);
            dontSaveAreaVisible = EditorGUILayout.IntSlider("Зона видимости: ", dontSaveAreaVisible, 1, 10);



            GUILayout.Space(15.0f);
            GUILayout.Label("Карта для редактирования"); GUILayout.Space(5.0f);
            editMap.DontSave = EditorGUILayout.ObjectField(editMap.DontSave, typeof(Map), false) as Map;

            GUILayout.Space(15.0f);
            GUILayout.Label("Список монстров закрепленых на карте"); GUILayout.Space(5.0f);
            _worldMonstrList.DontSave = EditorGUILayout.ObjectField(_worldMonstrList.DontSave, typeof(WorldMonstersList), false) as WorldMonstersList;

            GUILayout.Space(15.0f);
            GUILayout.Label("Список NPC"); GUILayout.Space(5.0f);
            _NPCList.DontSave = EditorGUILayout.ObjectField(_NPCList.DontSave, typeof(NPCsRegistry), false) as NPCsRegistry;

            GUILayout.Space(15.0f);
            GUILayout.Label("Список NPC закрепленых на карте"); GUILayout.Space(5.0f);
            _worldNPCList.DontSave = EditorGUILayout.ObjectField(_worldNPCList.DontSave, typeof(WorldNPCList), false) as WorldNPCList;


            GUILayout.Space(15.0f);
            GUILayout.Label("Список ресурсов закрепленых на карте"); GUILayout.Space(5.0f);
            _worldResourcesList.DontSave = EditorGUILayout.ObjectField(_worldResourcesList.DontSave, typeof(WorldResourcesList), false) as WorldResourcesList;

            GUILayout.Space(15.0f);
            GUILayout.Label("Список станков закрепленых на карте"); GUILayout.Space(5.0f);
            _machineList.DontSave = EditorGUILayout.ObjectField(_machineList.DontSave, typeof(MachineList), false) as MachineList;

            GUILayout.Space(15.0f);
            GUILayout.Label("Список предметов"); GUILayout.Space(5.0f);
            _itemsList.DontSave = EditorGUILayout.ObjectField(_itemsList.DontSave, typeof(ItemsRegistry), false) as ItemsRegistry;

            GUILayout.Space(20.0f);
            GUI.enabled = dontSaveAreaVisible != _areaViseble || editMap.IsDontSave() || _monstersList.IsDontSave() || _worldMonstrList.IsDontSave()
                || _resourceList.IsDontSave() || _worldResourcesList.IsDontSave() || _machineList.IsDontSave() || _itemsList.IsDontSave() ||
                _NPCList.IsDontSave() || _worldNPCList.IsDontSave();
            if (GUILayout.Button("Save"))
            {
                _areaViseble = dontSaveAreaVisible;
                PlayerPrefs.SetInt("EditAreaVisible", _areaViseble);

                editMap.SavePrefab();

                _monstersList.SavePrefab();

                _worldMonstrList.SavePrefab();

                _NPCList.SavePrefab();

                _worldNPCList.SavePrefab();

                _resourceList.SavePrefab();

                _worldResourcesList.SavePrefab();

                _machineList.SavePrefab();

                _itemsList.SavePrefab();

                WindowOpenWorld.ReloadMap();
            }

            EditorGUILayout.EndScrollView();
            GUI.enabled = true;
        }

        public static void Load()
        {

            editMap = new SettingCell("editMapPath");

            _areaViseble = PlayerPrefs.GetInt("EditAreaVisible", 1);
            dontSaveAreaVisible = _areaViseble;

            _monstersList = new SettingCell("editMonsterList");

            _worldMonstrList = new SettingCell("editWorldMonsterList");

            _NPCList = new SettingCell("editNPCList");

            _worldNPCList = new SettingCell("editWorldNPCList");

            _resourceList = new SettingCell("editResourcesList");

            _worldResourcesList = new SettingCell("editWorldResourcesList");

            _machineList = new SettingCell("editMachineList");

            _itemsList = new SettingCell("settingItemsList");

        }
    }
}
#endif