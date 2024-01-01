using Protocol;
using Protocol.MSG.Game.ToClient;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Target
{
    public class TargetStateReceiverService : MonoBehaviour
    {
        private NetworkManager m_networkManager;
        private TargetView m_targetView;

        [Inject]
        public void Construct(NetworkManager networkManager, TargetView targetView)
        {
            m_networkManager = networkManager;
            m_targetView = targetView;
        }

        private void Awake()
        {
            m_networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_STATE_SC, OnTargetState);
        }

        private void OnTargetState(Packet packet)
        {
            packet.Read(out MSG_UNIT_TARGET_STATE_SC targetState);

            Debug.Log($"TargetStateReceiverService.OnTargetState: {targetState}");

            m_targetView.SetTarget(targetState.TargetName, targetState.HP, targetState.MaxHP);
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_STATE_SC);
        }
    }
}
