using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Target.UI
{
    public class TargetView : MonoBehaviour
    {
        [SerializeField]
        private Text _nameLabel;
        [SerializeField]
        private Image _hpBar;
        private UnitTargetRequestSender _unitTargetRequestSender;
        private GameObject _targetViewWindow;
        private TargetListener _targetInfoService;

        [Inject]
        private void Construct(UnitTargetRequestSender unitTargetRequestSender, TargetListener targetInfoService)
        {
            _unitTargetRequestSender = unitTargetRequestSender;
            _targetInfoService = targetInfoService;
        }

        private void Awake()
        {
            _nameLabel = GetComponentInChildren<Text>();
            _targetViewWindow = transform.Find("ImageTarget").gameObject;
            _targetViewWindow.SetActive(false);

            _targetInfoService.OnTargetObjectChanged += OnTargetStateUpdated;
            _targetInfoService.OnTargetHPUpdated += UpdateTargetHp;
        }

        private void OnTargetStateUpdated(int objectId, string name, float percentHP)
        {
            UpdateTargetHp(percentHP);
            _nameLabel.text = name;

            _targetViewWindow.SetActive(objectId != 0);
        }

        private void UpdateTargetHp(float percentHP)
        {
            _hpBar.fillAmount = percentHP / 100f;
        }

        public void CancelTarget()
        {
            _unitTargetRequestSender.TargetOff();
        }
    }
}