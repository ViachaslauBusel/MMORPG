using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Protocol;
using Protocol.MSG.Game;
using RUCP;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace ServerList
{
    public class SL_Main : MonoBehaviour
    {

        public static SL_Main Instance { get; private set; }
        private string ip;
        private int port = 0;
        public Text info;
        public Button button_start;
        private NetworkManager m_networkManager;
        private ZenjectSceneLoader m_sceneLoader;
        private Client m_client;

        [Inject]
        public void Construct(NetworkManager networkManager, ZenjectSceneLoader sceneLoader) 
        {
           m_networkManager = networkManager;
           m_sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            info.text = "Нет доступных серверов";
            button_start.interactable = false;

            m_networkManager.RegisterHandler(Opcode.MSG_GAME_AUTHORIZATION, GameAuthorization);
        }

        private void GameAuthorization(Packet packet)
        {
            packet.Read(out MSG_GAME_AUTHORIZATION_SC response);

            switch (response.InformationCode)
            {
                case Protocol.Data.LoginInformationCode.AuthorizationSuccessful:
                    m_networkManager.ConnectTo(m_client);
                    m_sceneLoader.LoadSceneAsync("Lobby");
                    break;
                    default:
                    info.text = $"Не удалось подключиться к серверу. Код ошибки:{response.InformationCode}. Выберите другой сервер или попробуйте позже ..."; //Показать ошибку не удалось подключиться к серверу
                                                                                                                      //Включаем кнопки подключения к серверу
                    button_start.interactable = true;
                    break;
            }
        }

        public void SetAdress(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            info.text = "Нажмите чтобы продолжить";
            button_start.interactable = true;
        }

        public void OpenConnection()
        {
            StartCoroutine(IEReconnection());
        }

        private IEnumerator IEReconnection()
        {
            if (ip.Equals("")) yield break;
            if (port == 0) yield break;
            UnityEngine.Debug.Log("Подключение к ..." + ip + " : " + port);
            info.text = "Подключение к ..." + ip + " : " + port;
            button_start.interactable = false;


            m_client = new Client();
            m_client.SetHandler(() => m_networkManager.Profile);
            m_client.ConnectTo(ip, port);

            yield return new WaitWhile(() => m_client.Status == NetworkStatus.LISTENING);

            if (m_client.Status != NetworkStatus.CONNECTED)
            {
                info.text = "Не удалось подключиться к серверу. Выберите другой сервер или попробуйте позже ..."; //Показать ошибку не удалось подключиться к серверу
                                                                                                                  //Включаем кнопки подключения к серверу
                button_start.interactable = true;
                yield break;
            }

            //Успешное подключение
            MSG_GAME_AUTHORIZATION_CS request = new MSG_GAME_AUTHORIZATION_CS();
            request.LoginID = m_networkManager.LoginID;
            request.SessionToken = m_networkManager.Sessionkey;
            m_client.Send(request);
        }
    }
}