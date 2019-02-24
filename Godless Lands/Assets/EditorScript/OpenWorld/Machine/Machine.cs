#if UNITY_EDITOR
using Machines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    [System.Serializable]
    public class Machine
    {
        public int id;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public GameObject prefab;
        public MachineUse machineUse;

        public Machine(GameObject prefab, GameObject gameObject)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation.eulerAngles;
            scale = gameObject.transform.localScale;
            this.prefab = prefab;
            ActionMachine machine = gameObject.GetComponent<ActionMachine>();
             machineUse = machine.GetMachine();
        }
    }
}
#endif