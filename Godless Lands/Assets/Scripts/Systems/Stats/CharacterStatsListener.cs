using Protocol;
using Protocol.MSG.Game.ToClient.Stats;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Systems.Stats
{
    internal class CharacterStatsListener : MonoBehaviour
    {
        private NetworkManager m_networkManager;
        private CharacterStatsHolder m_characterStatsHolder;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            m_networkManager = networkManager;
            m_networkManager.RegisterHandler(Opcode.MSG_UPDATE_STATS, UpdateStats);
        }

        private void Awake()
        {
            m_characterStatsHolder = GetComponent<CharacterStatsHolder>();
        }

        private void UpdateStats(Packet packet)
        {
            packet.Read(out MSG_UPDATE_STATES update);

            foreach(var stat in update.Stats)
            {
                m_characterStatsHolder.SetStat(stat.Code, stat.Value);
            }
            m_characterStatsHolder.CallStatsUpdateEvent();
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_UPDATE_STATS);
        }
    }
}
