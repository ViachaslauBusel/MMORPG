using Network.Core;
using Protocol.MSG.Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lobby
{
    public class Character : MonoBehaviour
    {
        private Text txt_name;
        private int id;
        private Canvas canvas_main, canvas_character_cretor;
        private Button button;
        private NetworkManager m_networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            m_networkManager = networkManager;
        }

        private void Awake()
        {
            canvas_main = GameObject.Find("CanvasMain").GetComponent<Canvas>();
            canvas_character_cretor = GameObject.Find("CanvasCharacterCreator").GetComponent<Canvas>();
            txt_name = GetComponentInChildren<Text>();
            button = GetComponent<Button>();
            button.interactable = false;
        }


        public void SetCharacter(int id, string name)
        {
            this.id = id;
            this.txt_name.text = name;
            button.interactable = true;
        }

        public void ButTouch()
        {
            if (id == -1) { OpenCharCreator(); return; } //Если персонаж не создан

            MSG_SELECT_CHARACTER_CS request = new MSG_SELECT_CHARACTER_CS();
            request.CharacterID = id;
            m_networkManager.Client.Send(request);
        }



        private void OpenCharCreator()
        {
            canvas_main.enabled = false;
            canvas_character_cretor.enabled = true;
        }

    }
}