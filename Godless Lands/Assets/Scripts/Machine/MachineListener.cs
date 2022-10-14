using Items;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using Zenject;

namespace Machines
{
    public class MachineListener : MonoBehaviour
    {
        public Smelter smelter;
        public Smelter grindstone;
        public Smelter tannery;
        public Workbench workbench;
        private Machine selectMachine;
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
            networkManager.RegisterHandler(Types.MachineUse, MachineUseVoid);
            networkManager.RegisterHandler(Types.MachineAddComponent, MachineComponent);
            networkManager.RegisterHandler(Types.MachineClose, MachineClose);
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
            while (nw.AvailableBytesForReading > 0)
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
            networkManager?.UnregisterHandler(Types.MachineUse);
            networkManager?.UnregisterHandler(Types.MachineAddComponent);
            networkManager?.UnregisterHandler(Types.MachineClose);
        }
    }
}