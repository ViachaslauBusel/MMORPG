using Network.Core;
using Protocol;
using Protocol.MSG.Game;
using RUCP;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lobby
{
    public class CharacterCreator : MonoBehaviour
    {

        private Canvas canvas_main, canvas_character_cretor;
        private InputField input_name;
        private Button but_create_char;
        private NetworkManager m_networkManager;
        private ZenjectSceneLoader m_sceneLoader;

        [Inject]
        private void Construct(NetworkManager networkManager, ZenjectSceneLoader sceneLoader)
        {
            m_networkManager = networkManager;
            m_sceneLoader = sceneLoader;
            networkManager.RegisterHandler(Opcode.MSG_CREATE_CHARACTER, CharacterCreationEvent);
        }

        private void Start()
        {
            but_create_char = GameObject.Find("Button_Create").GetComponent<Button>();
            canvas_main = GameObject.Find("CanvasMain").GetComponent<Canvas>();
            canvas_character_cretor = GetComponent<Canvas>();
            canvas_character_cretor.enabled = false;

            input_name = GameObject.Find("InputName").GetComponent<InputField>();

          
        }

        private void CharacterCreationEvent(Packet packet)
        {
            packet.Read(out MSG_CREATE_CHARACTER_SC response);

            switch (response.InformationCode)
            {
                case Protocol.Data.LoginInformationCode.AuthorizationSuccessful:
                    m_sceneLoader.LoadSceneAsync("Lobby");
                    break;
                default:
                    ErrorCreator.ShowError(response.InformationCode);
                    but_create_char.interactable = true;
                    break;
            }
        }

        public void CreateCharacter()
        {

            if (input_name.text.Length <= 3 || input_name.text.Length > 30) { ErrorCreator.ShowError(1); return; }
            but_create_char.interactable = false;
            

            MSG_CREATE_CHARACTER_CS request = new MSG_CREATE_CHARACTER_CS();
            request.Name = input_name.text;
            m_networkManager.Client.Send(request);
        }

        public void ExitCreator()
        {
            canvas_main.enabled = true;
            canvas_character_cretor.enabled = false;
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_CREATE_CHARACTER);
        }
    }
}