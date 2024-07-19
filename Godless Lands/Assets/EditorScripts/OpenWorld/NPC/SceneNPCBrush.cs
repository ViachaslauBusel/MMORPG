#if UNITY_EDITOR
using NPCRedactor;
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldEditor
{
    public class SceneNPCBrush
    {
        private static GameObject brush;//Кисть с нпц
        private static NPCData selectedPrefab;//Выбранныйпрефаб для кисти
        private static Vector2 lastMousePos;


        /// <summary>
        /// Отрисовка кисти на сцене
        /// </summary>
        /// <param name="_camera"></param>
        public static void SceneGUI(Camera _camera)
        {


            if (WindowNPCBrush.IsFixationMode)//Если включен режим добовление НПЦ на карту
            {
                GameObject fixOBJ = WindowNPCBrush.editableNPC;
                
                Vector2 _pos = Event.current.mousePosition;
                _pos.y = _camera.pixelHeight - _pos.y;
                if (Event.current.type == EventType.MouseDown && Event.current.button == 1)//Если была нажата правая кнопка мыши
                {
                    lastMousePos = _pos;
                }
                else if (Event.current.type == EventType.MouseDrag && Event.current.button == 1)
                {
                    float delta = _pos.x - lastMousePos.x;
                    fixOBJ.transform.rotation = Quaternion.Euler(0.0f, fixOBJ.transform.rotation.eulerAngles.y + delta, 0.0f);
                    lastMousePos = _pos;
                }
                return;
            }


            RaycastHit hit;
            Vector2 position = Event.current.mousePosition;
            position.y = _camera.pixelHeight - position.y;
            Ray ray = _camera.ScreenPointToRay(position);
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            if (Physics.Raycast(ray, out hit, 1000.0f, layerMask))
            {

                if (brush == null || selectedPrefab != WindowNPCBrush.NPCPrefab)
                {
                    Destroy();
                    selectedPrefab = WindowNPCBrush.NPCPrefab;

                        brush = GameObject.Instantiate(selectedPrefab.Prefab.editorAsset);
                        brush.name = "BrushMonster";
                        //  brush.hideFlags = HideFlags.HideAndDontSave;
                }

                brush.transform.position = hit.point;

                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)//Если была нажата левая кнопка мыши. Включить режим закрепление НПЦ на карте
                {
                    WindowNPCBrush.editableNPC = brush;
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
            if (WindowNPCBrush.IsFixationMode)
            {
                GameObject.DestroyImmediate(WindowNPCBrush.editableNPC);
                WindowNPCBrush.editableNPC = null;
            }
                brush = null;
        }
    }
}
#endif