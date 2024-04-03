#if UNITY_EDITOR
using ObjectRegistryEditor.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ObjectRegistryEditor
{
    public class WindowAddObject : EditorWindow
    {
        private IGenericWindow m_genericWindow;
        /// <summary>
        /// Открыть окно для выбора объекта который наследуются от interface T
        /// </summary>
        public static void Display<T>(Action<T> action)
        {

            WindowAddObject window = EditorWindow.GetWindow<WindowAddObject>(true, "ADD");
            window.minSize = new Vector2(300.0f, 500.0f);
            GenericWindowAddObject<T> genericWindow = new GenericWindowAddObject<T>(action, window);
            window.m_genericWindow = genericWindow;

        }
        private void OnGUI()
        {
            m_genericWindow?.OnGUI();
        }
    }


    public class GenericWindowAddObject<T> : IGenericWindow
    {
        private string m_find = "";
        private Action<T> m_action;
        private EditorWindow m_window;
        private List<Type> m_objects = new List<Type>();

        public GenericWindowAddObject(Action<T> action, EditorWindow window)
        {
            m_action = action;
            m_window = window;
            Assembly assembly = typeof(T).Assembly;


            Type interfaceFind = typeof(T);
            var findTypes = assembly.GetTypes().Where((t) => t != interfaceFind && interfaceFind.IsAssignableFrom(t));
            m_objects.AddRange(findTypes);
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(Color.white));
            m_find = EditorGUILayout.TextField("FIND:", m_find).ToLower();
            EditorGUILayout.EndVertical();
            Type[] objects = string.IsNullOrEmpty(m_find) ? m_objects.ToArray()
                                                     : m_objects.Where((i) => i.Name.ToLower().Contains(m_find)).ToArray();
            for (int i = 0; i < objects.Length; i++)
            {
                EditorGUILayout.BeginHorizontal((i % 2 == 0) ? BackgroundStyle.Get(Color.gray) : BackgroundStyle.Get(Color.blue));
                GUILayout.Label($"{objects[i].Name}");
                if (GUILayout.Button("add", GUILayout.Width(60)))
                {
                    T obj = (T)Activator.CreateInstance(objects[i]);
                    if (obj != null) { m_action?.Invoke(obj); }
                    m_window.Close();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

}
#endif