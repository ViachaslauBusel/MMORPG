#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorld
{
    public class CreateMachineAsset
    {
        [MenuItem("Assets/Create/MachineList")]
        public static void Create()
        {
            CreateAsset.Create<MachineList>("MachineList");
        }
    }
}
#endif