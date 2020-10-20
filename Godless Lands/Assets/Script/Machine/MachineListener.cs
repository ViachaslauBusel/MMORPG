using Items;
using RUCP;
using RUCP.Handler;
using RUCP.Packets;
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
        public Smelter tannery;
        public Workbench workbench;
        private Machine selectMachine;

        private void Awake()
        {
            HandlersStorage.RegisterHandler(Types.MachineUse, MachineUseVoid);
            HandlersStorage.RegisterHandler(Types.MachineAddComponent, MachineComponent);
            HandlersStorage.RegisterHandler(Types.MachineClose, MachineClose);
        }

        private void MachineClose(Packet nw)
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

        private void MachineComponent(Packet nw)
        {
            if (selectMachine == null) return;
            bool component = nw.ReadBool();
            while (nw.AvailableBytes > 0)
            {
                int index = nw.ReadInt();
                Item item = nw.ReadItem();

                if (component) selectMachine.UpdateComponent(index, item);
                else selectMachine.UpdateFuel(index, item); 
            }
        }

        private void MachineUseVoid(Packet nw)
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
                case MachineUse.Workbench:
                    selectMachine = workbench;
                    break;
                case MachineUse.Tannery:
                    selectMachine = tannery;
                    break;
            }
            if (selectMachine != null) selectMachine.Open();
        }

        private void OnDestroy()
        {
            HandlersStorage.UnregisterHandler(Types.MachineUse);
            HandlersStorage.UnregisterHandler(Types.MachineAddComponent);
            HandlersStorage.UnregisterHandler(Types.MachineClose);
        }
    }
}