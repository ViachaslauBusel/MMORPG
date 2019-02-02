using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SL_Main : MonoBehaviour {

    private static SL_Main this_class;
    private string ip;
    private int port = 0;
    public Text info;
    public Button button_start;

    private void Start()
    {
        if (this_class == null)
            this_class = this;
        else
            Destroy(this);

        info.text = "Нет доступных серверов";
        button_start.interactable = false;
    }
    public static SL_Main Instance
    {
        get { return this_class; }
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

        info.text = "Подключение к ..." + ip+" : "+port;
        button_start.interactable = false;

        Client new_client = new Client(ip, port);
        NetworkWriter nw = new NetworkWriter(Channels.Connection);
        nw.write(NetworkManager.GetLoginId());
        nw.write(NetworkManager.GetKey());
        new_client.Connection(nw);

        //Ожидания подключения
        while (new_client.GetConnection() == -1)
        {
            yield return null;
        }
        if (new_client.IsConnection())//Успешное подключение
        {
            Client old_client = NetworkManager.Socket;
            NetworkManager.Socket = new_client;
            old_client.CloseWithoutDebug();
            SceneManager.LoadScene("Lobby");
        }
        else//Подключиться не удалось
        {
            new_client.CloseWithoutDebug();
           

            info.text = "Не удалось подключиться к серверу. Выберите другой сервер или попробуйте позже ..."; //Показать ошибку не удалось подключиться к серверу
                      //Включаем кнопки подключения к серверу
            button_start.interactable = true;
        }
    }
}
