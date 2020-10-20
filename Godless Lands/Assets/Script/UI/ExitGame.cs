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

   // public Text text;
   // private Canvas exitGame;
    public bool exit = false;

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.LobbyReload, LobbyReload);
    }

    private void LobbyReload(Packet nw)//Успешный выход в лобби на сервере
    {
     //   print("Load lobby");
        SceneManager.LoadScene("Lobby");
    }

    public void OnExit()
    {
        exit = true;
        StartCoroutine(IERestart("Вы действительно хотите выйти из игры?"));
    }
    public void OnReload()
    {
        exit = false;
        StartCoroutine(IERestart("Вы действительно хотите выйти в комнату выбора персонажа?"));
    }

    private IEnumerator IERestart(string title)
    {
        Confirm confirm = Confirm.instant;
        confirm.SetTitle(title);
        int answer = confirm.IsConfirm();
        while (answer < 0)
        {
            yield return 0;
            answer = confirm.IsConfirm();
        }
        if (answer == 1)//Yes
        {
            ButYes();
        }
    }
    private void ButYes()//Кнопка подтвердить выход или перезаход
    {
        if (exit)
        {
            Application.Quit();
            return;
        }

        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.LobbyReload);
        NetworkManager.Send(nw);
    }

 /*   public void ButNo()
    {
        exitGame.enabled = false;
    }*/

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.LobbyReload);
    }
}
