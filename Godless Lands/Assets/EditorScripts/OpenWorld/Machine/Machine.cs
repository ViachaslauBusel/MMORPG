#if UNITY_EDITOR
using UnityEngine;

namespace OpenWorldEditor
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
            React machine = gameObject.GetComponent<React>();
             machineUse = machine.GetMachine();
        }
    }
}
#endif