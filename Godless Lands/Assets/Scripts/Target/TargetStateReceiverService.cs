using Protocol;
using Protocol.MSG.Game.ToClient;
using Protocol.MSG.Game.ToClient.Target;
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
            m_networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_FULL_SC, OnTargetState);
            m_networkManager.RegisterHandler(Opcode.MSG_UNIT_TARGET_HP_SC, OnTargetChangeHP);
        }

        private void OnTargetChangeHP(Packet packet)
        {
           packet.Read(out MSG_UNIT_TARGET_HP_SC targetHP);

            Debug.Log($"TargetStateReceiverService.OnTargetChangeHP: {targetHP}");

            m_targetView.UpdateTarget(targetHP.PercentHP);
        }

        private void OnTargetState(Packet packet)
        {
            packet.Read(out MSG_UNIT_TARGET_FULL_SC targetState);

            Debug.Log($"TargetStateReceiverService.OnTargetState: {targetState}");

            m_targetView.SetTarget(targetState.TargetName, targetState.PercentHP);
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_FULL_SC);
            m_networkManager.UnregisterHandler(Opcode.MSG_UNIT_TARGET_HP_SC);
        }
    }
}
