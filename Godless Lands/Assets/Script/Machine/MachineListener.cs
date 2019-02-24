using Items;
using RUCP;
using RUCP.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Machines
{
    public class MachineListener : MonoBehaviour
    {
        public Smelter smelter;
        public Smelter grindstone;
        private Machine selectMachine;

        private void Awake()
        {
            RegisteredTypes.RegisterTypes(Types.MachineUse, MachineUseVoid);
            RegisteredTypes.RegisterTypes(Types.MachineAddComponent, MachineComponent);
            RegisteredTypes.RegisterTypes(Types.MachineClose, MachineClose);
        }

        private void MachineClose(NetworkWriter nw)
        {
            if (selectMachine == null) return;
            selectMachine.Hide();
        }


        /*   private void MachineFuel(NetworkWriter nw)
           {
               if (selectMachine == null) return;
               int index = nw.ReadInt();
               Item item = Inventory.GetItem(nw.ReadInt());
               int count = nw.ReadInt();
               selectMachine.PutFuel(index, item, count);
           }*/

        private void MachineComponent(NetworkWriter nw)
        {
            if (selectMachine == null) return;
            bool fuel = nw.ReadBool();
            int index = nw.ReadInt();
            Item item = Inventory.GetItem(nw.ReadInt());
            int count = nw.ReadInt();
            if(fuel) selectMachine.PutFuel(index, item, count);
            else selectMachine.PutComponent(index, item, count);
        }

        private void MachineUseVoid(NetworkWriter nw)
        {
            selectMachine = null;
            MachineUse machineUse = (MachineUse) nw.ReadByte();
            print(machineUse);
            switch (machineUse)
            {
                case MachineUse.Smelter:
                    selectMachine = smelter;
                    break;
                case MachineUse.Grindstone:
                    selectMachine = grindstone;
                    break;
            }
            if (selectMachine != null) selectMachine.Open();
        }

        private void OnDestroy()
        {
            RegisteredTypes.UnregisterTypes(Types.MachineUse);
            RegisteredTypes.UnregisterTypes(Types.MachineAddComponent);
            RegisteredTypes.UnregisterTypes(Types.MachineClose);
        }
    }
}