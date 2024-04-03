#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRegistryEditor
{
    public class ButtonDown
    {
        private static bool m_active = false;
        private static Vector2 m_clickPosition = Vector2.zero;

        /// <summary>
        /// Кнопка становиться активной при нажатии и удержании левой кнопки мыши, до тех пор пока кнопка не будет отпущена
        /// </summary>
        internal static bool IsActive(Rect rect, GUIStyle style)
        {
            Event e = Event.current;

            bool thisButtonActive = false;
            if (m_active)
            {
                Debug.Log("call");
                //Активна именно эта кнопка?
                thisButtonActive = rect.Contains(m_clickPosition);

                if (e.type == EventType.MouseUp)
                {
                    m_active = false;
                    thisButtonActive = false;
                    e.Use();
                    Debug.Log("Diactivate");
                }
            }
            else
            {
                //Если курсор мыши находится в окне 
                bool containsCursor = rect.Contains(e.mousePosition);
                if (containsCursor)
                {

                    //Если нажата левая кнопка мыши активировать кнопку
                    if (e.type == EventType.MouseDown && e.button == 0)
                    {
                        thisButtonActive = true;
                        m_clickPosition = e.mousePosition;
                        m_active = true;
                        GUI.changed = true;
                        //Перехватить события
                        e.Use();
                        Debug.Log("Activate");
                    }
                }
            }


            GUI.Label(rect, thisButtonActive ? style.active.background : style.normal.background);
            return thisButtonActive;

        }
    }
}
#endif