using Protocol;
using Protocol.MSG.Game.Drop;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Drop
{
    /// <summary>
    /// Listens comands from the server
    /// </summary>
    public class DropListener
    {
        private NetworkManager _networkManager;
        private DropWindow _dropWindow;

        [Inject]
        public DropListener(NetworkManager networkManager, DropWindow dropWindow)
        {
            _networkManager = networkManager;
            _dropWindow = dropWindow;

            _networkManager.RegisterHandler(Opcode.MSG_DROP_LIST_SYNC, OnDropListSync);
        }

        private void OnDropListSync(Packet packet)
        {
            packet.Read(out MSG_DROP_LIST_SYNC_SC drop_msg);

            _dropWindow.Open(drop_msg.SyncData);
        }
    }
}
