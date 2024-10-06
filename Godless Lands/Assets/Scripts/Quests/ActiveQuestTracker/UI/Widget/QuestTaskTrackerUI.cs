using TMPro;
using UnityEngine;

namespace Quests.TaskTracker.UI
{
    internal class QuestTaskTrackerUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _conditionDescription;
        private NodeTask _task;

        public void Init(NodeTask task)
        {
            _task = task;

            task.Handler.OnTaskProgressChanged += UpdateProgress;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            _conditionDescription.text = $"- ({_task.GetProgress()}) {_task.GetDescription()}";
        }

        private void OnDestroy()
        {
            _task.Handler.OnTaskProgressChanged -= UpdateProgress;
        }
    }
}
