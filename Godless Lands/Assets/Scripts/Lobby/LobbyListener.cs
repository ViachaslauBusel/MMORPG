using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RUCP.Packets;
using RUCP.Network;
using Loader;

namespace Lobby
{
    public class LobbyListener : MonoBehaviour
    {

        public GameObject gameLoader;
        public Text text_info;
        //  private bool order = false;
        private Character[] characters;

        private void Awake()
        {
            HandlersStorage.RegisterHandler(Types.MyCharacters, MyCharacters);
            HandlersStorage.RegisterHandler(Types.SelectCharacter, SelectCharacter);
        }

        private void SelectCharacter(Packet nw)
        {
            if (nw.ReadByte() == 0) //Если выбор персонажа прошел успешно
            {
                Instantiate(gameLoader).GetComponent<GameLoader>().LoadGame();//Загрузка карты
            }
            else
            {
                ErrorCreator.ShowError(3);//Ошибка выбора персонажа
            }
        }

        void Start()
        {
            characters = GetComponentsInChildren<Character>();
         

        }


        /// <summary>
        /// Получение списка персонажей ид + имя
        /// </summary>
        /// <param name="nw"></param>
        private void MyCharacters(Packet nw)
        {
            print("Пакет получен");
            for (int i = 0; i < characters.Length; i++)
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

       

        private void OnDestroy()
        {
            HandlersStorage.UnregisterHandler(Types.MyCharacters);
            HandlersStorage.UnregisterHandler(Types.SelectCharacter);
        }
    }
}