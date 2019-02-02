using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Lobby_CharacterCreator : MonoBehaviour {

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

        RegisteredTypes.RegisterTypes(Types.OwnCharacterCreate, OwnCharacterCreate);
    }

    private void OwnCharacterCreate(NetworkWriter nw)
    {
        int code = nw.ReadInt();
        if (code == 10) { SceneManager.LoadScene("Lobby"); return; }//ok TODO
        else { Lobby_ErrorCreator.ShowError(code); }
        but_create_char.interactable = true;
    }

    public void CreateCharacter()
    {

        if(input_name.text.Length <= 3 || input_name.text.Length > 30) { Lobby_ErrorCreator.ShowError(1); return; }
        but_create_char.interactable = false;

        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.OwnCharacterCreate);
        nw.write(input_name.text);

        NetworkManager.Send(nw);
    }

        public void ExitCreator()
    {
        canvas_main.enabled = true;
        canvas_character_cretor.enabled = false;
    }
}
