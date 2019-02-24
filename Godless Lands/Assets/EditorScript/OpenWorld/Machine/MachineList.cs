#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    public class MachineList : ScriptableObject
    {
        public List<Machine> machines;

        public void Add(Machine machine)
        {
            if (machines == null) { machines = new List<Machine>(); }
            while (machine.id < 1 || ConstainsID(machine.id)) machine.id++;

            machines.Add(machine);
        }

        public bool ConstainsID(int id)
        {
            foreach(Machine machine in machines)
            {
                if (machine.id == id) return true;
            }
            return false;
        }

        public void Remove(Machine machine)
        {
            machines.Remove(machine);
        }
        public void Remove(int id)
        {
            foreach (Machine machine in machines)
            {
                if (machine.id == id)
                {
                    machines.Remove(machine);
                    return;
                }
            }
        }

        public Machine this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= machines.Count) return null;
                return machines[index];
            }
        }
    }
}
#endif