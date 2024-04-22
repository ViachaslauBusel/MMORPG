using System;
using UI.ConfirmationDialog;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Messenger.Settings
{
    public class MessengerSettings : MonoBehaviour
    {
        public static MessengerSettings Instance { get; private set; }
        [SerializeField]
        private Button applyButton;
        private TabsContainer _editableContainer;
        private Canvas _canvas;
        private ConfirmationDialogController _confirmationDialog;

        public event Action update;


        public TabsContainer EditableTabs => _editableContainer;


        [Inject]
        private void Construct(ConfirmationDialogController confirmationDialog)
        {
            _confirmationDialog = confirmationDialog;
        }

        private void Awake()
        {
            Instance = this;
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            _canvas.enabled = false;
        }
        public void Open() {
            if (_canvas.enabled) return;
            _canvas.enabled = true;

            _editableContainer = TabsManager.Instance.ReadTabsContainer();

            applyButton.interactable = false;

            update?.Invoke();
        }
        public void Close()
        {
            if (applyButton.interactable)
            {
                _confirmationDialog.AddRequest(
                   $"Все несохраненные изменения будут потеряны. Закрыть?",
                   () =>
                   {
                       _canvas.enabled = false;
                   },
                   () => { }
                   );
            } else _canvas.enabled = false;
        }
        public void UpdateChanges()
        {
  
            applyButton.interactable = true;
            update?.Invoke();
        }
        public void ApplyChanges()
        {
            TabsManager.Instance.SaveTabsContainer(EditableTabs);
            applyButton.interactable = false;
        }
    }
}