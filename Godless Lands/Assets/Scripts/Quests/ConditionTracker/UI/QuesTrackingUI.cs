using TMPro;
using UnityEngine;
using Zenject;

namespace Quests.ConditionTracker.UI
{
    internal class QuesTrackingUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_informationBox;

        private Canvas m_canvas;
        private QuestConditionTrackingService m_questConditionTrackingService;

        [Inject]
        private void Construct(QuestConditionTrackingService questConditionTrackingService)
        {
            m_questConditionTrackingService = questConditionTrackingService;
        }

        private void Awake()
        {
            m_canvas = GetComponent<Canvas>();
            HideInformation();
        }

        internal void UpdateInformation(ITrackableQuestCondition trackableQuestCondition)
        {
            m_informationBox.text = trackableQuestCondition.GetInformation();
            m_canvas.enabled = true;
        }

        internal void HideInformation()
        {
            m_informationBox.text = string.Empty;
            m_canvas.enabled = false;
        }
    }
}
