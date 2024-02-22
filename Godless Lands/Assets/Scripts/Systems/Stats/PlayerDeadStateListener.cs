using Protocol;
using Protocol.MSG.Game.PlayerDeadState;
using Protocol.MSG.Game.ToClient;
using RUCP;
using UI.PlayerCharacterDead;
using UnityEngine;
using Zenject;

namespace Systems.Stats
{
    internal class PlayerDeadStateListener : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private PlayerCharacterDeadWindow _playerCharacterDeadWindow;



        [Inject]
        private void Construct(NetworkManager networkManager, PlayerCharacterDeadWindow playerCharacterDeadWindow)
        {
            _networkManager = networkManager;
            _playerCharacterDeadWindow = playerCharacterDeadWindow;
        }

        private void Awake()
        {
            _networkManager.RegisterHandler(Opcode.MSG_PLAYER_DEATH_STATE, PlayerDeadStateHandler);
        }

        private void PlayerDeadStateHandler(Packet packet)
        {
           packet.Read(out MSG_PLAYER_DEATH_STATE_SC msg);

            if(msg.IsAlive)
            {
               _playerCharacterDeadWindow.Close();
            }
            else
            {
               _playerCharacterDeadWindow.Open();
            }
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_PLAYER_DEATH_STATE);
        }
    }
}
