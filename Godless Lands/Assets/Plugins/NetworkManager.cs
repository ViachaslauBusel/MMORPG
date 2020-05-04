using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public static NetworkManager Instance { get; private set; }

    private static Client client = null;

    private static int sessionkey;
    private static int login_id;


    public static Client Socket
    {
        get
        {
            return client;
        }
        set
        {
            client = value;
        }
    }

    public static void SetKey(int key)
    {
        sessionkey = key;
    }
    public static int GetKey()
    {
        return sessionkey;
    }
    public static void SetLoginID(int id)
    {
        login_id = id;
    }
    public static int GetLoginId()
    {
        return login_id;
    }


    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        RegisteredTypes.RegisterUnknown(Unknown);
        enabled = false;
    }

    public static void Connection(string ip, int port)
    {
        client = new Client(ip, port);
        client.Connection();
        NetworkManager.Instance.enabled = true;
    }



    public static bool IsConnection()
    {
        if (client == null) return false;
        return client.IsConnection();
    }
    public static NetworkStatus GetConnection()
    {
        return client.GetNetworkStatus();
    }

    void Update()
    {

        NetworkWriter p = client.GetPack();
        if (p != null)
        {
            RegisteredTypes.Read(p.GetTypePack())(p);
        }
    }
    private void Unknown(NetworkWriter nw)
    {
        UnityEngine.Debug.Log("Тип пакета не распознан: " + nw.GetTypePack());
    }
    public static bool Send(NetworkWriter net_writer)
    {
        try
        {
            client.SendWriter(net_writer);
            return true;
        } catch(BufferOverflowException e)
        {
            UnityEngine.Debug.LogError(e);
        }
        return false;
    }
    private void OnDestroy()
    {
        print("Close socket");
        if (client != null) client.Close();
    }
}
