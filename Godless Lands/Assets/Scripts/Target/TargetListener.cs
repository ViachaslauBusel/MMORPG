using Network.Core;
using Nickname;
using Protocol;
using Protocol.MSG.Game.ToClient;
using Protocol.MSG.Game.ToClient.Target;
using RUCP;
using System;
using Units.Registry;

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

        private UnitVisualObjectRegistry _unitObjectRegistry;
        private NetworkManager _networkManager;

        public event Action<int, string, float> OnTargetObjectChanged;
        public event Action<float> OnTargetHPUpdated;

        public TargetListener(NetworkManager networkManager, UnitVisualObjectRegistry unitObjectRegistry)
        {
            _networkManager = networkManager;
            _unitObjectRegistry = unitObjectRegistry;

            _networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_FULL_SC, OnTargetState);
            _networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_HP_SC, OnTargetChangeHP);
        }

        private void OnTargetChangeHP(Packet packet)
        {
           packet.Read(out MSG_UNIT_TARGET_HP_SC targetHP);

            //Debug.Log($"TargetStateReceiverService.OnTargetChangeHP: {targetHP}");

            OnTargetHPUpdated?.Invoke(targetHP.PercentHP);
        }

        private void OnTargetState(Packet packet)
        {
            packet.Read(out MSG_UNIT_TARGET_FULL_SC targetState);

            //Debug.Log($"TargetStateReceiverService.OnTargetState: {targetState.TargetObjectID}");

            string targetName = _unitObjectRegistry.GetUnitVisualObjectByNetworkId(targetState.TargetObjectID)?.Nickname ?? "null";
            OnTargetObjectChanged?.Invoke(targetState.TargetObjectID, targetName, targetState.PercentHP);
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_FULL_SC);
            _networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_HP_SC);
        }
    }
}
