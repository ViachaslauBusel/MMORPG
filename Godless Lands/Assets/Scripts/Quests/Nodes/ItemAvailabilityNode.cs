using NodeEditor;
using Quests.ConditionTracker;
using Quests.Nodes;
using System;
using UnityEngine;
using Zenject;

[NodeGroup("Quests")]
public class ItemAvailabilityNode : QuestStageNode, ITrackableQuestCondition
{
    //[SerializeField]
    //private ItemData m_requiredItem;
    [SerializeField]
    private int m_requiredAmount;

  //  private PlayerItemStorage m_playerItemStorage;
    private bool m_isConditionConfirmed;
    private bool m_isStateUpdated;
    

    //[Inject]
    //private void Construct(PlayerItemStorage playerItemStorage)
    //{
    //    m_playerItemStorage = playerItemStorage;
    //}

    public string GetInformation()
    {
        return "";
        //var item = m_playerItemStorage.GetItemByData(m_requiredItem);
        //int itemAmount = item != null ? item.Count : 0;
        //return $"необходимо {m_requiredItem.Name} ({itemAmount} / {m_requiredAmount})";
    }

    public void StartTracking()
    {
        Debug.Log($"Start tracking {name}");
       // m_playerItemStorage.OnStorageUpdated += OnStorageUpdated;
    }

    private void OnStorageUpdated()
    {
        Debug.Log($"OnStorageUpdated {name}");
       m_isStateUpdated = true;
       m_isConditionConfirmed = CheckQuestCondition();
    }

    public void StopTracking() 
    {
        Debug.Log($"Stop tracking {name}");
        //m_playerItemStorage.OnStorageUpdated -= OnStorageUpdated;
    }

    // Вызывается из QuestConditionTrackingService для обновления состояния отслеживания. Возвращает true, если состояние изменилось
    public void UpdateTracking(out bool isStateUpdated, out bool isConditionConfirmed)
    {
        isStateUpdated = UpdateState(ref m_isStateUpdated);
        isConditionConfirmed = UpdateState(ref m_isConditionConfirmed);
    }

    private bool UpdateState(ref bool state)
    {
        if (state)
        {
            state = false;
            return true;
        }
        return false;
    }

    // Вызывается из системы диалогов
    public bool CheckQuestCondition()
    {
        return false;// m_playerItemStorage.ContainsItem(m_requiredItem);
    }
}

