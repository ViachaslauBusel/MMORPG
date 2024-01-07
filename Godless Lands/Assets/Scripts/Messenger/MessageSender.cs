using Protocol.MSG.Game.Messenger;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Messenger
{
    class MessageSender: MonoBehaviour
    {
        [SerializeField] InputField inputField;
        [SerializeField] Dropdown dropdown;
        private NetworkManager _networkManager;


        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Return))
            {
                if (inputField.isFocused)
                {
                    if (inputField.text.Length == 0) return;
                    MsgLayer layer = dropdown.value switch
                    {
                        0 => MsgLayer.AroundMsg,
                        1 => MsgLayer.AllMsg,
                        _ => MsgLayer.AroundMsg,
                    };
                    MSG_MESSAGE_CS msg = new MSG_MESSAGE_CS();
                    msg.Layer = layer;
                    msg.Message = inputField.text;
                    _networkManager.Client.Send(msg);

                    inputField.text = "";
                }
                else
                {
                    inputField.Select();
                    inputField.ActivateInputField();
                }
            }
        }
    }
}
