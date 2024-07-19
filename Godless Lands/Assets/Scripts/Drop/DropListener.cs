using Drop.UI;
using Network.Core;
using Protocol;
using Protocol.MSG.Game.Drop;
using RUCP;
using System;
using Zenject;

namespace Drop
{
    /// <summary>
    /// Listens comands from the server
    /// </summary>
    public class DropListener : IDisposable
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

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_DROP_LIST_SYNC);
        }
    }
}
