using UI.ConfirmationDialog;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Messenger.Settings
{
    public class TabRedactor : MonoBehaviour
    {
        public static TabRedactor Instance { get; private set; }
        [SerializeField] InputField inputName;
        [SerializeField] Slider inputFontSize;
        private LayerField[] layers;
        private Tab selectTab;
        private ConfirmationDialogController _confirmationDialog;

        [Inject]
        private void Construct(ConfirmationDialogController confirmationDialog)
        {
            _confirmationDialog = confirmationDialog;
        }

        private void Awake()
        {
            Instance = this;
            layers = GetComponentsInChildren<LayerField>();
        }
        private void Start()
        {
            MessengerSettings.Instance.update += UpdateTabs;
        }
        private void UpdateTabs()
        {
            if (MessengerSettings.Instance.EditableTabs.Contains(selectTab))
                 Select(selectTab);
            else Select(MessengerSettings.Instance.EditableTabs[0]);
        }
        public void Select(Tab tab)
        {
            selectTab = tab;

            inputName.SetTextWithoutNotify(tab.name);
            inputName.onEndEdit.RemoveAllListeners();
            inputName.onEndEdit.AddListener((str) =>
            {
                tab.name = str;
                MessengerSettings.Instance.UpdateChanges();
            });
            inputFontSize.SetValueWithoutNotify(tab.fontSize);
            inputFontSize.onValueChanged.RemoveAllListeners();
            inputFontSize.onValueChanged.AddListener((v) =>
            {
                tab.fontSize = (int)v;
                MessengerSettings.Instance.UpdateChanges();
            });

            foreach(LayerField layerField in layers)
            {
                Layer lay = tab.GetLay(layerField.Layer);
                layerField.Usage.SetIsOnWithoutNotify(lay.use);
                layerField.Color.SetValueWithoutNotify((int)lay.color);

                layerField.Usage.onValueChanged.RemoveAllListeners();
                layerField.Usage.onValueChanged.AddListener((u) => { lay.use = u; MessengerSettings.Instance.UpdateChanges(); });

                layerField.Color.onValueChanged.RemoveAllListeners();
                layerField.Color.onValueChanged.AddListener((c) => { lay.color = (MsgColor)c; MessengerSettings.Instance.UpdateChanges(); });
            }
        }

        public void Delete()
        {
            if (selectTab == null) return;
            _confirmationDialog.AddRequest(
                $"Вы уверены что хотите удалить вкладку {selectTab.name}?",
                () => { 
                    MessengerSettings.Instance.EditableTabs.Remove(selectTab);
                    MessengerSettings.Instance.UpdateChanges();
                },
                () => { }
                );
        }
    }
}