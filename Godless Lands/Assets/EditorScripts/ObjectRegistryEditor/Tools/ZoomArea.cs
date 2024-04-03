#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using static UnityEditor.GenericMenu;

namespace ObjectRegistryEditor
{
    public class ZoomArea
    {
        private static ZoomArea m_instance = null;
        private const float MIN_SCALE = 0.5f, MAX_SCALE = 2.0f;
        private RectOffset m_indent;
        private Vector2 m_size;
        private Matrix4x4 m_prevGuiMatrix;
        private float m_scale = 1.0f;
        private Vector2 m_position = Vector2.zero;
        /// <summary>Размер окна</summary>
        private Rect m_window;
        private GenericMenu m_menu = new GenericMenu();
        private Vector2 m_mousePosition = Vector2.zero;
        /// <summary>Координаты на эране(мониторе) начала окна </summary>
        private Vector2 m_startScreenPosition = Vector2.zero;

        /// <summary>
        /// Позиция мыши на заднем полотне
        /// </summary>
        public Vector2 MousePosition => m_mousePosition;
        public Event EventCurrent { get; private set; }
        public float Scale => m_scale;


        /// <param name="indent">Отступы от краев окна (левый, верхний, правый, нижний)</param>
        public ZoomArea(RectOffset indent, Vector2 size)
        {
            m_indent = indent;
            m_size = size;
            m_window = new Rect(m_indent.left, m_indent.top, Width(m_scale), Height(m_scale));
        }

        public void AddMenuItem(string name, MenuFunction func)
        {
            m_menu.AddItem(new GUIContent(name), false, func);
        }
        public void ClearMenu()
        {
            m_menu = new GenericMenu();
        }

        public void Begin()
        {
            EventCurrent = Event.current;
            m_window.width = Width(m_scale);
            m_window.height = Height(m_scale);

            m_prevGuiMatrix = GUI.matrix;

            GUI.EndGroup();


            GUI.BeginGroup(m_window);

            m_startScreenPosition = GUIUtility.GUIToScreenPoint(m_window.position * (1.0f - m_scale));


            Matrix4x4 translation = Matrix4x4.TRS(m_window.position, Quaternion.identity, Vector3.one);
            Matrix4x4 scaleMatrix = Matrix4x4.Scale(new Vector3(m_scale, m_scale, 1.0f));
            GUI.matrix = translation * scaleMatrix * translation.inverse * GUI.matrix;

            //Передвигаемая подложка
            GUI.BeginGroup(new Rect(m_position.x, m_position.y, m_size.x, m_size.y));
            // Debug.Log($"AREA:{clippedArea}");

            m_mousePosition = Event.current.mousePosition;




            Grid.Draw(new Vector2(m_size.x, m_size.y));
            m_instance = this;
        }
        /// <summary>
        /// Преобразует координаты экранного пространства(монитора) в координаты на подложке
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public static Vector2 GetCanvasPosition(Vector2 screenPosition)
        {
            if (m_instance == null) return Vector2.zero;
            return screenPosition - m_instance.m_startScreenPosition - m_instance.m_position;
        }
        public void End()
        {
            m_instance = null;
            GUI.EndGroup();
            GUI.matrix = m_prevGuiMatrix;



            GUI.EndGroup();
            m_scale = CalculateEvent(m_scale);

            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        }


        /// <summary>
        /// Получить ширину области видимости в пикселях в зависимости от масштаба
        /// </summary>
        /// <param name="scale">Текущий маштаб</param>
        private float Width(float scale)
        {
            return (Screen.width - (m_indent.right + m_indent.left)) * (1.0f / scale);
        }
        /// <summary>
        /// Получить высоту области видимости в пикселях в зависимости от масштаба
        /// </summary>
        /// <param name="scale">Текущий маштаб</param>
        private float Height(float scale)
        {
            return (Screen.height - (m_indent.bottom + m_indent.top)) * (1.0f / scale);
        }

        /// <summary>
        /// Обработка событий манипуляций мышь.
        /// </summary>
        private float CalculateEvent(float scale)
        {

            if (Event.current.type == EventType.ScrollWheel)
            {

                scale = Mathf.Clamp(scale + Event.current.delta.y * 0.01f, MIN_SCALE, MAX_SCALE);


                //Смещение позиции окна при скролле по позиции курсора
                Vector2 mousePosition = Event.current.mousePosition * (1.0f / scale);
                Vector2 point = Rect.PointToNormalized(m_window, mousePosition);
                Vector2 delta = new Vector2(Width(scale) - m_window.width, Height(scale) - m_window.height) * point;
                m_position += delta;
                //   Vector2 dif = (mousePosition * (1.0f / _s)) - (mousePosition * (1.0f / scale));



                //  Debug.Log($"Center:{point}");

                //  Debug.Log(mousePosition);
                //  mouseScroll?.Invoke(dif);

                GUI.changed = true;
            }
            if (Event.current.type == EventType.MouseDrag && Event.current.button == 2)
            {
                m_position += Event.current.delta;


                GUI.changed = true;
                //  Debug.Log($"delata:{Event.current.delta}");
            }
            if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
            {
                m_menu.ShowAsContext();
            }

            if (m_position.x > 0.0f) m_position.x = 0.0f;
            if (m_position.y > 0.0f) m_position.y = 0.0f;
            float borderX = -m_size.x + m_window.width;
            if (m_position.x < borderX) m_position.x = borderX;
            float borderY = -m_size.y + m_window.height;
            if (m_position.y < borderY) m_position.y = borderY;

            // m_mousePosition = (Event.current.mousePosition * (1.0f / scale)) - m_position - (new Vector2(m_indent.left, m_indent.top) * (1.0f / scale));
            return scale;
        }




    }
    public static class RectExtensions
    {
        public static Vector2 TopLeft(this RectOffset rect)
        {
            return new Vector2(rect.left, rect.top);
        }
    }
}
#endif