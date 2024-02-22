using Protocol;
using Protocol.Data.Stats;
using Protocol.MSG.Game.ToClient.Stats;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.PlayerCharacterDead;
using UnityEngine;
using Zenject;

namespace Systems.Stats
{

    /// <summary>
    /// Listens for server messages to synchronize the player character's stats.
    /// </summary>
    internal class CharacterStatsListener : MonoBehaviour
    {
        private NetworkManager m_networkManager;
        private CharacterStatsHolder m_characterStatsHolder;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            m_networkManager = networkManager;
        }

        private void Awake()
        {
            m_characterStatsHolder = GetComponent<CharacterStatsHolder>();
            m_networkManager.RegisterHandler(Opcode.MSG_UPDATE_STATS, UpdateStats);
            m_networkManager.RegisterHandler(Opcode.MSG_LOAD_STATES, LoadStats);
        }

        private void LoadStats(Packet packet)
        {
            packet.Read(out MSG_LOAD_STATES loadStates);

            m_characterStatsHolder.SetStat(StatCode.Name, loadStates.CharacterName);

            foreach(var stat in loadStates.Stats)
            {
                m_characterStatsHolder.SetStat(stat.Code, stat.Value);
            }
            m_characterStatsHolder.CallStatsUpdateEvent();
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
            m_networkManager.UnregisterHandler(Opcode.MSG_LOAD_STATES);
        }
    }
}
