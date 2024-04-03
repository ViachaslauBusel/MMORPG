#if UNITY_EDITOR
using ObjectRegistryEditor.Tools;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ObjectRegistryEditor
{
    /// <summary>
    /// Окно для выбора объекта данных из контейнера
    /// </summary>
    public class WindowAddEditableObject : EditorWindow 
    {
        private IGenericWindow m_genericWindow;
        /// <summary>
        /// Открыть окно для выбора объекта данных из контейнера
        /// </summary>
        /// <param name="objectList">Контейнер в котором нужно выбрать обьект</param>
        /// <param name="action">Действия выполняемое над выбранным обьектом</param>
        public static void Display<T>(EditableObjectRegistry<T> objectList, Action<T> action) where T : ScriptableObject, IEditableObject, new()
        {

            WindowAddEditableObject window = EditorWindow.GetWindow<WindowAddEditableObject>(true, "ADD");
            window.minSize = new Vector2(300.0f, 500.0f);
            GenericWindow<T> genericWindow  = new GenericWindow<T>();
            genericWindow.m_action = action;
            genericWindow.m_objectList = objectList;
            genericWindow.m_window = window;
            window.m_genericWindow = genericWindow;
          
        }
        private void OnGUI()
        {
            m_genericWindow.OnGUI();
        }


    }

    public class GenericWindow<T> : IGenericWindow where T : ScriptableObject, IEditableObject, new()
    {
        private string m_find = "";
        public Action<T> m_action;
        public EditableObjectRegistry<T> m_objectList;
        public EditorWindow m_window;

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(Color.white));
            m_find = EditorGUILayout.TextField("FIND:", m_find).ToLower();
            EditorGUILayout.EndVertical();
            T[] objects = string.IsNullOrEmpty(m_find) ? m_objectList.Objects.ToArray()
                                                     : m_objectList.Objects.Where((i) => i.Name.ToLower().Contains(m_find)).ToArray();
            for (int i = 0; i < objects.Length; i++)
            {
                EditorGUILayout.BeginHorizontal((i % 2 == 0) ? BackgroundStyle.Get(Color.gray) : BackgroundStyle.Get(Color.blue));
                GUILayout.Label(objects[i].Preview, GUILayout.Width(40), GUILayout.Height(40));
                GUILayout.Label($"{objects[i].Name}:{objects[i].ID}");
                if (GUILayout.Button("add", GUILayout.Width(60)))
                {
                    m_action?.Invoke(objects[i]);
                    m_window.Close();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    public interface IGenericWindow
    {
        void OnGUI();
    }
}
#endif