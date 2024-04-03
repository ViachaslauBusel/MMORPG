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
        /// ������ ����������� �������� ��� ������� � ��������� ����� ������ ����, �� ��� ��� ���� ������ �� ����� ��������
        /// </summary>
        internal static bool IsActive(Rect rect, GUIStyle style)
        {
            Event e = Event.current;

            bool thisButtonActive = false;
            if (m_active)
            {
                Debug.Log("call");
                //������� ������ ��� ������?
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
                //���� ������ ���� ��������� � ���� 
                bool containsCursor = rect.Contains(e.mousePosition);
                if (containsCursor)
                {

                    //���� ������ ����� ������ ���� ������������ ������
                    if (e.type == EventType.MouseDown && e.button == 0)
                    {
                        thisButtonActive = true;
                        m_clickPosition = e.mousePosition;
                        m_active = true;
                        GUI.changed = true;
                        //����������� �������
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