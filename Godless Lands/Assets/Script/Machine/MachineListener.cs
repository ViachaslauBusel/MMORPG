﻿using Items;
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
        public Smelter tannery;
        public Workbench workbench;
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
            bool component = nw.ReadBool();
            while (nw.AvailableBytes > 0)
            {
                int index = nw.ReadInt();
                Item item = nw.ReadItem();

                if (component) selectMachine.UpdateComponent(index, item);
                else selectMachine.UpdateFuel(index, item); 
            }
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
            RegisteredTypes.UnregisterTypes(Types.MachineUse);
            RegisteredTypes.UnregisterTypes(Types.MachineAddComponent);
            RegisteredTypes.UnregisterTypes(Types.MachineClose);
        }
    }
}