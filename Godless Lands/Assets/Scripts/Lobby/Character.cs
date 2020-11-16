using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RUCP;
using System;
using RUCP.Network;
using RUCP.Packets;

namespace Lobby
{
    public class Character : MonoBehaviour
    {


        private Text txt_name;
        private int id;

        private Canvas canvas_main, canvas_character_cretor;
        private Button button;

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

            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.SelectCharacter);
            nw.WriteInt(id);
            NetworkManager.Send(nw);


        }



        private void OpenCharCreator()
        {
            canvas_main.enabled = false;
            canvas_character_cretor.enabled = true;
        }

    }
}