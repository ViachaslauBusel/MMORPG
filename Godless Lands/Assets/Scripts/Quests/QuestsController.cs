using Network.Core;
using Protocol;
using Protocol.MSG.Game.Quests;
using RUCP;
using Tasks;
using UnityEngine;

namespace Quests
{
    internal class QuestsController
    {
        private NetworkManager _networkManager;
        private int _questIdToIncrease;
        private TaskAwaiter _taskQuestToIncrease;

        public QuestsController(NetworkManager networkManager)
        {
            _networkManager = networkManager;

            _networkManager.RegisterHandler(Opcode.MSG_QUEST_STAGE_UP_RESPONSE, QuestUpResponse);
        }

        internal TaskAwaiter IncreaseQuestStage(int questId)
        {
            TaskAwaiter taskAwaiter = new TaskAwaiter();

            //If there is already a quest being increased, return false
            if (_questIdToIncrease != 0)
            {
                taskAwaiter.SetResult(false);
                return taskAwaiter;
            }

            _questIdToIncrease = questId;
            _taskQuestToIncrease = taskAwaiter;

            MSG_QUEST_STAGE_UP_REQUEST_CS request = new MSG_QUEST_STAGE_UP_REQUEST_CS();
            request.QuestID = questId;
            _networkManager.Client.Send(request);

            return taskAwaiter;
        }

        private void QuestUpResponse(Packet packet)
        {
            packet.Read(out MSG_QUEST_STAGE_UP_RESPONSE_SC response);

            if (response.QuestID == _questIdToIncrease)
            {
                _taskQuestToIncrease.SetResult(response.Result);
                _questIdToIncrease = 0;
            }
            else Debug.LogError($"Quest ID mismatch: {_questIdToIncrease} != {response.QuestID}");
        }
    }
}