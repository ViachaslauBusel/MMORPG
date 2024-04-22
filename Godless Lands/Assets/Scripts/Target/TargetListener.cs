using Protocol;
using Protocol.MSG.Game.ToClient;
using Protocol.MSG.Game.ToClient.Target;
using RUCP;
using System;

namespace Target
{
    public class TargetListener : IDisposable
    {
        public struct TargetInfo
        {
            public int objectId;
            public string TargetName;
            public float PercentHP;
        }

        private NetworkManager _networkManager;
        private TargetInfo _targetInfo;

        public event Action<int, string, float> OnTargetObjectChanged;
        public event Action<float> OnTargetHPUpdated;

        public TargetListener(NetworkManager networkManager)
        {
            _networkManager = networkManager;

            _networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_FULL_SC, OnTargetState);
            _networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_HP_SC, OnTargetChangeHP);
        }

        private void OnTargetChangeHP(Packet packet)
        {
           packet.Read(out MSG_UNIT_TARGET_HP_SC targetHP);

            //Debug.Log($"TargetStateReceiverService.OnTargetChangeHP: {targetHP}");
            _targetInfo.PercentHP = targetHP.PercentHP;

            OnTargetHPUpdated?.Invoke(_targetInfo.PercentHP);
        }

        private void OnTargetState(Packet packet)
        {
            packet.Read(out MSG_UNIT_TARGET_FULL_SC targetState);

            //Debug.Log($"TargetStateReceiverService.OnTargetState: {targetState.TargetObjectID}");

            _targetInfo.objectId = targetState.TargetObjectID;
            _targetInfo.TargetName = targetState.TargetName;
            _targetInfo.PercentHP = targetState.PercentHP;
            OnTargetObjectChanged?.Invoke(_targetInfo.objectId, _targetInfo.TargetName, _targetInfo.PercentHP);
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_FULL_SC);
            _networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_HP_SC);
        }
    }
}
