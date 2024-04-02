using UnityEditor;

namespace NodeEditor.Inspector
{
    public class PropertyContainer : IPropertyContainer
    {
        private SerializedProperty m_serializedProperty;

        public PropertyContainer(SerializedProperty serializedProperty)
        {
            m_serializedProperty = serializedProperty;
        }

        public void OnGUI()
        {
            EditorGUILayout.PropertyField(m_serializedProperty);
        }
    }
}