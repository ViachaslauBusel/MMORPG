#if UNITY_EDITOR
using MonsterRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Walkers.Monster;

namespace OpenWorldEditor
{

    public class MonsterSceneGUI
    {
        private static GameObject brush;
        private static MonsterData selectedMonster;
       


        public static void SceneGUI(Camera _camera)
        {
          

            if (WindowMonsters.MonsterPaint == null)//Если добавление монстров на карту не включен
            {
                Destroy();
                return;
            }


            RaycastHit hit;
            Vector2 position = Event.current.mousePosition;
            position.y = _camera.pixelHeight - position.y;
            Ray ray = _camera.ScreenPointToRay(position);
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
            {

                if (brush == null || selectedMonster != WindowMonsters.MonsterPaint)
                {
                    Destroy();
                    selectedMonster = WindowMonsters.MonsterPaint;

                    if (brush == null)
                    {
                       // brush = GameObject.Instantiate(selectedMonster.Prefab);
                        brush.name = "BrushMonster";
                      //  brush.hideFlags = HideFlags.HideAndDontSave;
                    }
                }

                brush.transform.position = hit.point;

                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)//Если была нажата левая кнопка мыши
                {

                   
                    WindowMonsters.editableMonster = brush;
                    brush = null;
                    WindowOpenWorld.window.Repaint();
                }
            }
            else
            {
                Destroy();
            }
        }

        

        public static void Destroy()
        {
            if (brush != null) GameObject.DestroyImmediate(brush);
            brush = null;
        }
    }
}
#endif