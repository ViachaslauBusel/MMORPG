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
        print("Пакет получен");
       for(int i=0; i<characters.Length; i++)
        {
            int ID = -1;
            string charName = "Создать персонажа";
            if (nw.AvailableBytes >= 8)
            {
                 ID = nw.ReadInt();
                 charName = nw.ReadString();
            }
            characters[i].SetCharacter(ID, charName);
        }
       // order = false;
    }

    private  void ConnectionLobby()
    {
        print("Пакет отправлен");
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
