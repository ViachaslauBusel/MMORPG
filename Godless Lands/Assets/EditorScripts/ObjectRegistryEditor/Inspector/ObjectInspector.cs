//#if UNITY_EDITOR
//using UnityEditor;

//namespace ObjectRegistryEditor.Inspector
//{
//    /// <summary>
//    /// Отрисовка в окне инпектора -> ObjectEditor
//    /// </summary>
//    [CustomEditor(typeof(ObjectEditor))]
//    [CanEditMultipleObjects]
//    public class ObjectInspector: Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            ((ObjectEditor)target).OBJ?.Draw();
//        }
//    }
//}
//#endif