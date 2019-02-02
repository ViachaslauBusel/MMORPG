using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

    public GameObject gameLoader;
    public Text text_info;
  //  private bool order = false;
    private Lobby_Character[] characters;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.MyCharacters, MyCharacters);
        RegisteredTypes.RegisterTypes(Types.SelectCharacter, SelectCharacter);
    }

    private void SelectCharacter(NetworkWriter nw)
    {
        if (nw.ReadByte() == 0) //Если выбор персонажа прошел успешно
        {
            Instantiate(gameLoader).GetComponent<GameLoader>().LoadGame();//Загрузка карты
        }
        else
        {
            Lobby_ErrorCreator.ShowError(3);//Ошибка выбора персонажа
        }
    }

    void Start () {
        characters = GetComponentsInChildren<Lobby_Character>();
        ConnectionLobby();
        
    }


    /// <summary>
    /// Получение списка персонажей ид + имя
    /// </summary>
    /// <param name="nw"></param>
    private void MyCharacters(NetworkWriter nw)
    {
       for(int i=0;  nw.AvailableBytes > 0; i++)
        {
            if (i == characters.Length) break;
            characters[i].SetCharacter(nw.ReadInt(), nw.ReadString());
        }
       // order = false;
    }

    private  void ConnectionLobby()
    {
        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.LobbyEntrance);
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.MyCharacters);
        RegisteredTypes.UnregisterTypes(Types.SelectCharacter);
    }
}
