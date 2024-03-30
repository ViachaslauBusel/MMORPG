using Protocol;
using Protocol.Data.Workbenches;
using Protocol.MSG.Game.Workbench;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench
{
    public class WorkbenchListener : IDisposable
    {
        private NetworkManager _networkManager;

        public event Action<WorkbenchType, bool, bool> OnWindowCommand;

        public WorkbenchListener(NetworkManager networkManager)
        {
            _networkManager = networkManager;

            _networkManager.RegisterHandler(Opcode.MSG_WORKBENCH_TOGGLE_WINDOW, ToggleWindow);
        }

        private void ToggleWindow(Packet packet)
        {
            packet.Read(out MSG_WORKBENCH_TOGGLE_WINDOW_SC msg);

            OnWindowCommand?.Invoke(msg.WorkbenchType, msg.IsOpen, msg.IsReadyForWork);
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_WORKBENCH_TOGGLE_WINDOW);
        }
    }
}
