using RUCP;
using UnityEngine;

namespace Machines
{
    public class ActionMachine : MonoBehaviour, React
    {
        // private Smelter smelter;
        public MachineUse machineUse;
        public int id;

        private void Awake()
        {
        //    smelter = GameObject.Find("SmelterGUI").GetComponent<Smelter>();
            //ActionListener.Add(this);
        }

        public Vector3 position
        {
            get { return transform.position; }
        }
        public float distance
        {
            get
            {
                return 2.2f;
            }
        }

        public void Use()
        {
            //smelter.Open();
            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.MachineUse);
            //nw.WriteInt(id);
            //NetworkManager.Send(nw);
        }

        private void OnDestroy()
        {
            //ActionListener.Remove(this);
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public MachineUse GetMachine()
        {
            return machineUse;
        }
    }
}