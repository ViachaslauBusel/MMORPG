using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ObjectRegistryEditor
{
    public static class OnOpenAssetNotifier
    {
        public static void Notify(ScriptableObject scriptableObject)
        {
            int instanceID = scriptableObject.GetInstanceID();
            int line = 1;
            // Find all methods marked with [OnOpenAsset] attribute
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        var attributes = method.GetCustomAttributes(typeof(OnOpenAssetAttribute), false);
                        if (attributes.Length > 0)
                        {
                            // Check the method parameters to call it correctly
                            var parameters = method.GetParameters();
                            bool result = false;

                            if (parameters.Length == 1)
                            {
                                result = (bool)method.Invoke(null, new object[] { instanceID });
                            }
                            else if (parameters.Length == 2)
                            {
                                result = (bool)method.Invoke(null, new object[] { instanceID, line });
                            }
                            else if (parameters.Length == 3)
                            {
                                result = (bool)method.Invoke(null, new object[] { instanceID, line, 0 });
                            }

                            if (result)
                            {
                                // If the method returns true, stop the loop
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}