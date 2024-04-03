#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ObjectRegistryEditor
{
    /// <summary>
    /// ObjectRegistry editor window.
    /// </summary>
    public class WindowRegistryEditor: EditorWindow
    {
        /// <summary>Cell width.</summary>
        private int _cellWidth = 100;
        /// <summary>Cell height.</summary>
        private int _cellHeight = 140;
        private float _menuHeight = 30.0f;
        private int _currentPage = 0;
        private int _totalPages;
        private IEditableObjectRegistry _editableRegistry;
        private IEditableObject _select_editor;
        private long _clickTime;

        protected void OnEnable()
        {
           // titleContent.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/bag.png"); //Resources.Load("Editor/Img/bag") as Texture2D;
        }

        public void OnGUI()
        {
            DrawMenu();
            if (_editableRegistry == null) return;
            DrawWindowGrid();
        }

        public static WindowRegistryEditor OpenWindow()
        {
            var window = EditorWindow.GetWindow<WindowRegistryEditor>(false, "ObjectRegistry");
            window.minSize = new Vector2(500.0f, 320.0f);
            return window;
        }

        public void DrawMenu()
        {

            GUILayout.BeginHorizontal(RedactorStyle.Menu);
            GUILayout.Space(10.0f);
            //Button save>>>
            if (GUILayout.Button("", RedactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                Save();
            }
            //Button save<<<<
            GUILayout.Space(20.0f);
            //Button export>>>
            if (GUILayout.Button("", RedactorStyle.Export, GUILayout.Height(25), GUILayout.Width(25)))
            {
                bool error = false;
                try
                {
                    _editableRegistry.Export();
                } catch(Exception ex)
                {
                    Debug.LogError(ex);
                    error = true; }
               finally { EditorUtility.DisplayDialog("Export", "Экспорт выполнен " +((error) ? "с ошибками" : "успешно"), "ok"); }
            }
            //Button export<<<<
            GUILayout.Space(20.0f);
            //Button remove>>>>
            if (GUILayout.Button("", RedactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            {
                RemoveSelectObject();
            }
            //Button remove<<<<
            GUILayout.Space(40.0f);

            //Выбор редактируемого хранилище
          //  _editableRegistry = EditorGUILayout.ObjectField(_editableRegistry, typeof(Container), false) as Container;
            GUILayout.FlexibleSpace();
            //Pages>>>
            if (GUILayout.Button("", RedactorStyle.Left, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (_currentPage > 0) _currentPage--;
            }
            GUILayout.Space(5.0f);
            GUILayout.Label((_currentPage + 1) + "/" + (_totalPages + 1), RedactorStyle.Text);
            GUILayout.Space(5.0f);
            if (GUILayout.Button("", RedactorStyle.Right, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (_currentPage < _totalPages) _currentPage++;
            }
            //Pages<<<
            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        private void DrawWindowGrid()
        {
            //Высота окна
            float height = position.height - _menuHeight;

            //Количество ячеек по горизонтали
            int horizontal_element = (int)position.width / _cellWidth;
            //Количество ячеек по вертикали
            int verctical_element = (int)height / _cellHeight;

            //Свободное пространство между ячейками
            float horizontal_space = (position.width - (horizontal_element * _cellWidth)) / (horizontal_element - 1);
            float vertical_space = (height - (verctical_element * _cellHeight)) / (verctical_element - 1);

            //Количество ячеек на странице
            int elementOnPage = horizontal_element * verctical_element;
            //Всего страниц
            _totalPages = (_editableRegistry.Count + 1) / (elementOnPage);
            //Если выбранная страница находится за последней допустимой
            if (_currentPage > _totalPages) _currentPage = _totalPages;

            //Индекс в хранилище отрисовымаего объекта
            int index = _currentPage * elementOnPage;
            int ver_i = 0;
            int hor_i = 0;
            for (ver_i = 0; ver_i < verctical_element; ver_i++)
            {
                for (hor_i = 0; hor_i < horizontal_element; hor_i++)
                {
                    //Если в хранилище закончились обьекты, отрисовать кнопку создание нового обьекта
                    if (_editableRegistry.Count <= index)
                    {
                        if (GUI.Button(new Rect(_cellWidth * hor_i + horizontal_space * hor_i + 18,
                                        _cellHeight * ver_i + vertical_space * ver_i + 38 + _menuHeight,
                                                     64, 64), "", RedactorStyle.Plus))
                        {
                            CreateObject();
                        }
                        return;//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    }
                    DrawItemArea(_editableRegistry[index++], ver_i, hor_i, horizontal_space, vertical_space);
                }
            }
        }
        /// <summary>
        /// Отрисовка ячейки с информацией об объекте
        /// </summary>
        /// <param name="obj">Обьект для отрисовки</param>
        /// <param name="ver_i">Позиция ячейки по вертикали</param>
        /// <param name="hor_i">Позиция ячейки по горизонтали</param>
        /// <param name="horizontal_space"></param>
        /// <param name="vertical_space"></param>
        private void DrawItemArea(IEditableObject obj, int ver_i, int hor_i, float horizontal_space, float vertical_space)
        {
            GUILayout.BeginArea(new Rect(_cellWidth * hor_i + horizontal_space * hor_i,
                                         _cellHeight * ver_i + vertical_space * ver_i + _menuHeight,
                                                     _cellWidth, _cellHeight),
                                                 (_select_editor != null && obj.Equals(_select_editor)) ? RedactorStyle.BackgraundSelectItem : RedactorStyle.BackgraundItem);

            DrawObject(obj);

            if (GUI.Button(new Rect(_cellWidth * hor_i + horizontal_space * hor_i,
                                    _cellHeight * ver_i + vertical_space * ver_i + _menuHeight,
                                                    _cellWidth, _cellHeight), "", RedactorStyle.Hide))
            {
                if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _clickTime < 500 && _select_editor == obj)
                {
                    //Двойной клик по sciptable object
                    ScriptableObject scriptableObject = obj as ScriptableObject;
                    AssetDatabase.OpenAsset(scriptableObject);
                }
                _clickTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                SelectObject(obj);
            }

        }

        /// <summary>
        /// Сохранить хранилище объектов
        /// </summary>
        private void Save() {
            if (_editableRegistry == null) return;
            AssetDatabase.Refresh();
            EditorUtility.SetDirty((ScriptableObject)_editableRegistry);
            AssetDatabase.SaveAssets();
        }
        public virtual void Export()
        {

        }
        /// <summary>
        /// Создание нового обьекта
        /// </summary>
        private void CreateObject() {
            IEditableObject obj = _editableRegistry.AddObject();
            SelectObject(obj);
        }

        /// <summary>
        /// Выбрать этот обьект для редактирование в окне Инспектора
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void SelectObject(IEditableObject obj) {
           // if (_select_editor == null) _select_editor = ScriptableObject.CreateInstance<ObjectEditor>();
            _select_editor = obj;
            //Необходима для вызова отрисовки в окне инспектор
            EditorUtility.SetDirty((ScriptableObject)_select_editor);
            Selection.activeObject = (ScriptableObject)_select_editor;
        }
        /// <summary>
        /// Отрисовать информацию о объекте
        /// </summary>
        /// <param name="obj"></param>
        private void DrawObject(IEditableObject obj)
        {
            GUILayout.Label(obj.Preview, GUILayout.Width(90), GUILayout.Height(90));
            Color def = GUI.contentColor;
            GUI.contentColor = Color.black;
            GUILayout.Label("ID: " + obj.ID);
            GUILayout.Label("Имя: " + obj.Name);
            GUI.contentColor = def;
            GUILayout.EndArea();
        }
        /// <summary>
        /// Удалить редактируемый объект в инспекторе из хранилище
        /// </summary>
        private void RemoveSelectObject() {
            if (_select_editor != null)
            {
                if (EditorUtility.DisplayDialog("Удаление предмета", "Удалить предмет ID: " + _select_editor.ID + ", Имя: " + _select_editor.Name + "?", "Да", "Нет"))
                {
                    _editableRegistry.RemoveObject((IEditableObject)_select_editor);
                    _select_editor = null;
                    Selection.activeObject = null;
                }
            }
            else EditorUtility.DisplayDialog("Удаление предмета", "Предмет не выбран", "OK");
        }

        [OnOpenAsset(1)]
        public static bool OpenGameStateFlow(int instanceID, int line)
        {
            // Get the asset path and type
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            Type assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);

            // Check if the asset type is NodesContainer
            bool isObjectRegistry = typeof(IEditableObjectRegistry).IsAssignableFrom(assetType);

            if (isObjectRegistry)
            {
                // Create and load the window if the asset is a NodesContainer
                WindowRegistryEditor window = OpenWindow();
                window.LoadFromFile(assetPath);
            }


            return isObjectRegistry;
        }

        private void LoadFromFile(string assetPath)
        {
            IEditableObjectRegistry container = (IEditableObjectRegistry)AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            _editableRegistry = container;
        }
    }
}
#endif