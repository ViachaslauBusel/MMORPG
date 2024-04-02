using NodeEditor.Data;
using Quests.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests.Journal
{
    internal class QuestHandler
    {
        private int m_questID;
        private QuestData m_questData;
        private int m_currentStageID;
        private List<int> m_stagesLog = new();

        internal int ID => m_questID;
        internal QuestData Data => m_questData;
        internal int CurrentStageID => m_currentStageID;
        internal IEnumerable<int> CompletedStages => m_stagesLog;

        internal QuestHandler(int questID, QuestData quest)
        {
            m_questID = questID;
            m_questData = quest;
        }

        private bool IsQuestCompleted()
        {
            return m_currentStageID == -1;
        }

        private bool IsQuestNotStarted()
        {
            return m_currentStageID == 0;
        }

        internal bool UpQuestStage()
        {
            if (IsQuestCompleted())
            {
                Debug.LogError("Quest has already been completed");
                return false;
            }

            // If the quest has not been started yet
            if (IsQuestNotStarted())
            {
               return StartQuest();
            }

            // Get the current node with the quest level
            QuestStageNode currentNode = m_questData.GetNodeByID(m_currentStageID) as QuestStageNode;

            if (currentNode == null)
            {
                Debug.LogError("Current node is null. Quest was emergency reset");
                // Emergency quest reset
                m_currentStageID = 0;
                return false;
            }

            QuestStageNode nextNode = currentNode.Next as QuestStageNode;

            return MoveToNextStage(currentNode, nextNode);
        }
        private bool StartQuest()
        {
            if (m_questData.StartNode != null && m_questData.StartNode.Next != null)
            {
                QuestStageNode firstNode = (QuestStageNode)m_questData.StartNode.Next;
                return MoveToNextStage(null, firstNode);
            }
            else
            {
                Debug.LogError("Quest start node is null");
                return false;
            }
        }

        private bool MoveToNextStage(QuestStageNode from, QuestStageNode to)
        {
            if (to != null)
            {
                // Quest conditions are not met
                if (!CheckQuestCondition(to)) return false;
            }

            // Add the current quest node to the log
            if (from != null)
            { m_stagesLog.Add(from.ID); }

            // If there is no next quest node, the quest is considered completed
            m_currentStageID = (to == null) ? -1 : to.ID;

            return true;
        }

        private bool CheckQuestCondition(Node currentNode)
        {
            if (currentNode is IQuestConditionNode questCondition)
            {
                bool condition = questCondition.CheckQuestCondition();

                if (!condition)
                {
                    Debug.Log("Quest conditions are not met");
                    return false;
                }
            }
            return true;
        }

        internal Quest GetQuest()
        {
            return new Quest(m_questID, m_questData, m_currentStageID, m_stagesLog);
        }
    }
}
