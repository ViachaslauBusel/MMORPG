using NodeEditor.CustomInspector;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace NodeEditor.Inspector
{
    internal class DropdownPropertyContainer : IPropertyContainer
    {
        private int m_selectedIndex;
        private String[] m_names;
        private int[] m_values;
        private FieldInfo m_field;
        private UnityEngine.Object m_target;

        public DropdownPropertyContainer(UnityEngine.Object target, FieldInfo field, DropdownOptionValue[] list)
        {
            // Initialize the names and values arrays with the exact size of the list
            m_names = new string[list.Length];
            m_values = new int[list.Length];
            m_target = target;
            m_field = field;

            // Use a for loop instead of LINQ for better performance
            for (int i = 0; i < list.Length; i++)
            {
                m_names[i] = list[i].Name;
                m_values[i] = list[i].Value;
            }

            // Get the selected index
            m_selectedIndex = Array.IndexOf(m_values, (int)m_field.GetValue(m_target));
        }

        public void OnGUI()
        {
            // Draw the popup and set the selected value
            m_selectedIndex = EditorGUILayout.Popup(m_selectedIndex, m_names);
            m_field.SetValue(m_target, m_values[m_selectedIndex]);
        }
    }
}