using NodeEditor.CustomInspector;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NodeEditor.Inspector
{
    [CustomEditor(typeof(Node))]
    public class NodeContentRenderer : Editor
    {
        private List<IPropertyContainer> m_propertyList;

        private void OnEnable()
        {
            m_propertyList = new List<IPropertyContainer>();

            // Check which class the target object inherits from, if it's not Node then add all its fields until we reach Node
            var type = target.GetType();
            while (type != null && type != typeof(Node))
            {
                m_propertyList.AddRange(GetPropertiesFrom(type));
                type = type.BaseType;
            }
        }

        private List<IPropertyContainer> GetPropertiesFrom(Type type)
        {
            List<IPropertyContainer> properties = new List<IPropertyContainer>();

            // Get all fields of the target object
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute(typeof(HideInInspector)) != null || field.GetCustomAttribute(typeof(PortAttribute)) != null)
                {
                    continue;
                }
                // Check if the field is private, if yes then check if it has the SerializeField attribute, if not skip it
                if (field.IsPrivate && field.GetCustomAttribute(typeof(SerializeField)) == null)
                {
                    continue;
                }
                // Check if the field has the DropdownAttribute, if yes then make custom dropdown for this field
                if (field.GetCustomAttribute(typeof(DropdownAttribute)) is DropdownAttribute dropdownAttribute)
                {
                    // Get the method specified in the DropdownAttribute
                    var dropdownOptionsMaker = target.GetType().GetMethod(dropdownAttribute.Function, BindingFlags.NonPublic | BindingFlags.Instance);

                    if (dropdownOptionsMaker != null)
                    {
                        var list = dropdownOptionsMaker.Invoke(target, null) as DropdownOptionValue[];
                        if (list != null)
                        {
                            properties.Add(new DropdownPropertyContainer(target, field, list));
                        }
                        else
                        {
                            Debug.LogWarning($"Method {dropdownAttribute.Function} return null in {target.GetType().Name}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Method {dropdownAttribute.Function} not found in {target.GetType().Name}");
                    }
                    continue;
                }
                var property = serializedObject.FindProperty(field.Name);
                if (property != null)
                {
                    properties.Add(new PropertyContainer(property));
                }
            }

            return properties;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            foreach (var property in m_propertyList)
            {
                property.OnGUI();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}