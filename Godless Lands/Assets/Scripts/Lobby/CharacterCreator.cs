using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Lobby
{
    public class CharacterCreator : MonoBehaviour
    {

        private Canvas canvas_main, canvas_character_cretor;
        private InputField input_name;
        private Button but_create_char;
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
            networkManager.RegisterHandler(Types.OwnCharacterCreate, OwnCharacterCreate);
        }

        private void Start()
        {
            but_create_char = GameObject.Find("Button_Create").GetComponent<Button>();
            canvas_main = GameObject.Find("CanvasMain").GetComponent<Canvas>();
            canvas_character_cretor = GetComponent<Canvas>();
            canvas_character_cretor.enabled = false;

            input_name = GameObject.Find("InputName").GetComponent<InputField>();

          
        }

        private void OwnCharacterCreate(Packet nw)
        {
            int code = nw.ReadInt();
            if (code == 10) { SceneManager.LoadScene("Lobby"); return; }//ok TODO
            else { ErrorCreator.ShowError(code); }
            but_create_char.interactable = true;
        }

        public void CreateCharacter()
        {

            if (input_name.text.Length <= 3 || input_name.text.Length > 30) { ErrorCreator.ShowError(1); return; }
            but_create_char.interactable = false;

            //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.OwnCharacterCreate);
            //nw.WriteString(input_name.text);

            //NetworkManager.Send(nw);
        }

        public void ExitCreator()
        {
            canvas_main.enabled = true;
            canvas_character_cretor.enabled = false;
        }

        private void OnDestroy()
        {
            networkManager.UnregisterHandler(Types.OwnCharacterCreate);
        }
    }
}