using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NodeEditor
{
    public class WindowNodeEditor : EditorWindow
    {

        private ZoomArea m_zoomArea;
        //List of current nodes for editing
        private List<NodeDrawer> m_nodes;
        private NodesContainer m_container;

        //[MenuItem("Window/Editors/NodeEditor")]
        public static WindowNodeEditor CreateWindow()
        {
            WindowNodeEditor window = EditorWindow.GetWindow<WindowNodeEditor>(false, "NodeEditor");
            window.minSize = new Vector2(500.0f, 320.0f);
            window.Initialize();
            return window;
        }

        private void OnEnable()
        {
            Initialize();
        }
        private void Initialize()
        {
            m_zoomArea = new ZoomArea(new RectOffset(0, 0, 50, 0), new Vector2(10_000.0f, 4000.0f));
            m_nodes = new List<NodeDrawer>();
        }

        internal void LoadFromFile(string assetPath)
        {
            NodesContainer container = AssetDatabase.LoadAssetAtPath<NodesContainer>(assetPath);
            SelectNodesContainer(container);
        }

        public void SelectNodesContainer(NodesContainer container)
        {
            m_container = container;

            if (m_container == null) return;

            m_container.Validate();

            //Load all nodes from the container for editing
            m_nodes.Clear();
            for (int i = 0; i < container.NodesCount; i++) { m_nodes.Add(NodeDrawer.Create(this, container.GetNodeByIndex(i))); }

            //Create dropdown menu for this nods type
            m_zoomArea.ClearMenu();
            var containerInfo = m_container.GetType().GetCustomAttribute<NodeGroupAttribute>();
            string groupName = containerInfo?.Group;
            List<Type> nodeTypes = NodeHelper.GetNodesByGroup(groupName);

            foreach(var nodeType in nodeTypes) 
            {
                m_zoomArea.AddMenuItem($"Create/{nodeType.Name}", () =>
                {
                    var node = NodeHelper.CreateNode(nodeType);

                    node.Position = m_zoomArea.MousePosition;

                    container.AddNode(node);
                    m_nodes.Add(NodeDrawer.Create(this, node));
                });
            }
        }

        /// <summary>
        /// Remove node with a specified ID
        /// </summary>
        /// <param name="nodeID"></param>
        public void RemoveNode(int nodeID)
        {
            //Remove from editing nodes
            m_nodes.RemoveAll((np) => np.Node.ID == nodeID);
            //Remove from container
            m_container?.RemoveNode(nodeID);
        }

        private void OnGUI()
        {
            DrawMenu();
            m_zoomArea.Begin();
            { 
                foreach (NodeDrawer i_node in m_nodes)
                {
                    i_node.Draw(m_zoomArea);
                    i_node.ProcessEvents(m_zoomArea.EventCurrent);
                }
                NodeConnectionDrawer.Draw(m_nodes);
            }
            m_zoomArea.End();
        }
        public void DrawMenu()
        {

            GUILayout.BeginHorizontal(RedactorStyle.Menu);
            GUILayout.Space(10.0f);
            //Button save>>>
            if (GUILayout.Button("", RedactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                m_container.SaveAsset();
            }
            //Button save<<<<
            GUILayout.Space(20.0f);
            //Button export>>>
            //if (GUILayout.Button("", RedactorStyle.Export, GUILayout.Height(25), GUILayout.Width(25)))
            //{
            //    bool error = false;
            //    try
            //    {
            //        Export();
            //    }
            //    catch (Exception) { error = true; }
            //    finally { EditorUtility.DisplayDialog("Export", "Экспорт выполнен " + ((error) ? "с ошибками" : "успешно"), "ok"); }
            //}
            //Button export<<<<
            //GUILayout.Space(20.0f);
            //Button remove>>>>
            //if (GUILayout.Button("", RedactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            //{
            //    RemoveSelectObject();
            //}
            //Button remove<<<<
           // GUILayout.Space(40.0f);

            //Выбор редактируемого хранилище
            //objectList = EditorGUILayout.ObjectField(objectList, typeof(Container), false) as Container;
            //GUILayout.FlexibleSpace();
            ////Pages>>>
            //if (GUILayout.Button("", RedactorStyle.Left, GUILayout.Height(25), GUILayout.Width(25)))
            //{
            //    if (m_page_cur > 0) m_page_cur--;
            //}
            //GUILayout.Space(5.0f);
            //GUILayout.Label((m_page_cur + 1) + "/" + (m_page_all + 1), RedactorStyle.Text);
            //GUILayout.Space(5.0f);
            //if (GUILayout.Button("", RedactorStyle.Right, GUILayout.Height(25), GUILayout.Width(25)))
            //{
            //    if (m_page_cur < m_page_all) m_page_cur++;
            //}
            //Pages<<<
            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        [OnOpenAsset(1)]
        public static bool OpenGameStateFlow(int instanceID, int line)
        {
            // Get the asset path and type
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            Type assetType = AssetDatabase.GetMainAssetTypeAtPath(assetPath);

            // Check if the asset type is NodesContainer
            bool isNodesContainer = typeof(NodesContainer).IsAssignableFrom(assetType);

            if (isNodesContainer)
            {
                // Create and load the window if the asset is a NodesContainer
                var window = CreateWindow();
                window.LoadFromFile(assetPath);
            }


            return isNodesContainer;
        }
    }
}