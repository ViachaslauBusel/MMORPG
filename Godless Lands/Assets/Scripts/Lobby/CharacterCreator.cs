using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using RUCP.Packets;
using RUCP.Network;

namespace Lobby
{
    public class CharacterCreator : MonoBehaviour
    {

        private Canvas canvas_main, canvas_character_cretor;
        private InputField input_name;
        private Button but_create_char;

        private void Start()
        {
            but_create_char = GameObject.Find("Button_Create").GetComponent<Button>();
            canvas_main = GameObject.Find("CanvasMain").GetComponent<Canvas>();
            canvas_character_cretor = GetComponent<Canvas>();
            canvas_character_cretor.enabled = false;

            input_name = GameObject.Find("InputName").GetComponent<InputField>();

            HandlersStorage.RegisterHandler(Types.OwnCharacterCreate, OwnCharacterCreate);
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

            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.OwnCharacterCreate);
            nw.WriteString(input_name.text);

            NetworkManager.Send(nw);
        }

        public void ExitCreator()
        {
            canvas_main.enabled = true;
            canvas_character_cretor.enabled = false;
        }
    }
}