using Network.Core;
using Protocol;
using Protocol.MSG.Game.Professions;
using Protocol.MSG.Game.Quests;
using RUCP;
using System;
using UnityEngine;

namespace Quests
{
    internal class QuestsListener : IDisposable
    {
        private QuestsModel _questsModel;
        private NetworkManager _networkManager;

        public QuestsListener(QuestsModel questsModel, NetworkManager networkManager)
        {
            _questsModel = questsModel;
            _networkManager = networkManager;
            _networkManager.RegisterHandler(Opcode.MSG_QUESTS_SYNC, QuestsUpdate);
            _networkManager.RegisterHandler(Opcode.MSG_QUESTS_LOAD, QuestsLoad);
        }

        private void QuestsLoad(Packet packet)
        {
            Debug.Log("QuestsLoad");
            packet.Read(out MSG_QUESTS_LOAD_SC msg);

            foreach (var quest in msg.Quests)
            {
                _questsModel.UpdateQuest(quest.QuestId, quest.StageID);
            }
            foreach (var quest in msg.CompletedQuests)
            {
                _questsModel.UpdateQuest(quest, -1);
            }

            _questsModel.NotifyQuestUpdates();
        }

        private void QuestsUpdate(Packet packet)
        {
            Debug.Log("QuestsUpdate");
            packet.Read(out MSG_QUESTS_SYNC_SC msg);

            foreach (var quest in msg.Quests)
            {
                _questsModel.UpdateQuest(quest.QuestId, quest.StageID);
            }
            _questsModel.NotifyQuestUpdates();
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_QUESTS_SYNC);
            _networkManager.UnregisterHandler(Opcode.MSG_QUESTS_LOAD);
        }
    }
}
