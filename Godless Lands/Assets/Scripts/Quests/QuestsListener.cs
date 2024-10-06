using Network.Core;
using Protocol;
using Protocol.MSG.Game.Professions;
using Protocol.MSG.Game.Quests;
using RUCP;
using System;

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
        }

        private void QuestsUpdate(Packet packet)
        {
            packet.Read(out MSG_QUESTS_SYNC_SC msg);

            foreach (var quest in msg.Quests)
            {
                _questsModel.UpdateQuest(quest.QuestId, quest.StageID, quest.Flag);
            }
            _questsModel.NotifyQuestUpdates();
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_QUESTS_SYNC);
        }
    }
}
