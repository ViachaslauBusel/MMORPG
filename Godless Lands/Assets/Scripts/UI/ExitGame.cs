using RUCP;
using RUCP.Handler;
using RUCP.Network;
using RUCP.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.LobbyReload, LobbyReload);
    }

    private void LobbyReload(Packet nw)//Успешный выход в лобби на сервере
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnExit()
    {
        Confirm.Instance.Subscribe(
        "Вы действительно хотите выйти из игры?",
        () => {
            Application.Quit();
        },
        () => { }
        );
    }
    public void OnReload()
    {
        Confirm.Instance.Subscribe(
        "Вы действительно хотите выйти в комнату выбора персонажа?",
        () => {
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.LobbyReload);
            NetworkManager.Send(nw);
        },
        () => { }
        );
    }


    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.LobbyReload);
    }
}
