using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger.Settings
{
    public class MessengerSettings : MonoBehaviour
    {
        public static MessengerSettings Instance { get; private set; }
        [SerializeField] Button applyButton;
        private TabsContainer editableContainer;
        private Canvas canvas;

        public event Action update;


        public TabsContainer EditableTabs => editableContainer;

        private void Awake()
        {
            Instance = this;
            canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            canvas.enabled = false;
        }
        public void Open() {
            if (canvas.enabled) return;
            canvas.enabled = true;

            editableContainer = TabsManager.Instance.ReadTabsContainer();

            applyButton.interactable = false;

            update?.Invoke();
        }
        public void Close()
        {
            if (applyButton.interactable)
            {
                Confirm.Instance.Subscribe(
                   $"Все несохраненные изменения будут потеряны. Закрыть?",
                   () =>
                   {
                       canvas.enabled = false;
                   },
                   () => { }
                   );
            } else canvas.enabled = false;
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