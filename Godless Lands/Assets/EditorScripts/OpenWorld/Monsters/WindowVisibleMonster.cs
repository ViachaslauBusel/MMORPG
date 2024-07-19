#if UNITY_EDITOR
using MonsterRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WindowVisibleMonster
    {
        private static MonstersLoader monstersLoader;
        private static int selectId = 0;

        public static void Draw()
        {
            if(monstersLoader == null)
            {
                GameObject obj = GameObject.Find("MapEditor");
                if(obj != null)
                {
                    monstersLoader = obj.GetComponent<MonstersLoader>();
                }
            }
            if (monstersLoader == null || monstersLoader.monsters == null) return;
            foreach(MonsterDrawGizmos monsterDraw in monstersLoader.monsters)
            {
                //GUILayout.Space(10.0f);
                //GUILayout.BeginHorizontal();
                //Monster _monster = WindowSetting.monstersList.GetMonster(monsterDraw.worldMonster.id);
                //GUILayout.Label(_monster.name + ": " + _monster.id);

                //GUI.enabled = selectId != monsterDraw.worldMonster.GetHashCode();
                //if (GUILayout.Button("Select", GUILayout.Width(60.0f)))
                //{
                //    Selection.activeObject = monsterDraw.gameObject;
                //    selectId = monsterDraw.worldMonster.GetHashCode();
                //    return;
                //}
                //GUI.enabled = true;

                //if (GUILayout.Button("Delet", GUILayout.Width(60.0f)))
                //{
                //    WindowSetting.WorldMonsterList.Remove(monsterDraw.worldMonster);//Удалить монстра из списка монстров карты
                //    MonsterVisibleSceneGUI.UpdateMonsterLoader();//Обновить монстров на сцене
                //    return;
                //}
                //GUILayout.EndHorizontal();
            }
        }
    }
}
#endif