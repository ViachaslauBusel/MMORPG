#if UNITY_EDITOR
using NPCRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    public class NPCSceneGUI
    {
        private static GameObject brush;
        private static NPC selectedNPC;



        public static void SceneGUI(Camera _camera)
        {


            if (WindowNPC.NPCPaint == null)//Если добавление NPC на карту не включен
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

                if (brush == null || selectedNPC != WindowNPC.NPCPaint)
                {
                    Destroy();
                    selectedNPC = WindowNPC.NPCPaint;

                    if (brush == null)
                    {
                        brush = GameObject.Instantiate(selectedNPC.prefab);
                        brush.name = "BrushMonster";
                        //  brush.hideFlags = HideFlags.HideAndDontSave;
                    }
                }

                brush.transform.position = hit.point;

                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)//Если была нажата левая кнопка мыши
                {


                    WindowNPC.editableNPC = brush;
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