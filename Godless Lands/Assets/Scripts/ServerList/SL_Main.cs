using RUCP;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ServerList
{
    public class SL_Main : MonoBehaviour
    {

        public static SL_Main Instance { get; private set; }
        private string ip;
        private int port = 0;
        public Text info;
        public Button button_start;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            info.text = "Нет доступных серверов";
            button_start.interactable = false;
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

            //TODO msg
            //ServerSocket new_client = new ServerSocket(ip, port);
            //Packet nw = new Packet(Channel.Connection);
            //nw.WriteInt(NetworkManager.LoginID);
            //nw.WriteInt(NetworkManager.Sessionkey);
            //new_client.Connection(nw);

            ////Ожидания подключения
            //while (new_client.NetworkStatus == NetworkStatus.LISTENING)
            //{
            //    yield return null;
            //}
            //if (new_client.IsConnected())//Успешное подключение
            //{
            //    ServerSocket old_client = NetworkManager.Client;
            //    NetworkManager.Client = new_client;
            //    old_client.Close();
            //    SceneManager.LoadScene("Lobby");
            //}
            //else//Подключиться не удалось
            //{
            //    new_client.Close();


            //    info.text = "Не удалось подключиться к серверу. Выберите другой сервер или попробуйте позже ..."; //Показать ошибку не удалось подключиться к серверу
            //                                                                                                      //Включаем кнопки подключения к серверу
            //    button_start.interactable = true;
            //}
        }
    }
}