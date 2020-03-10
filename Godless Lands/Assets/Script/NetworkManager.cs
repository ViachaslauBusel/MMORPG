using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    private static NetworkManager manager = null;

    private static Client client = null;
    private RegisteredTypes registered_types;

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
        if (manager != null) Destroy(this);
        else
        {
            manager = this;
            DontDestroyOnLoad(this);
        }
        registered_types = RegisteredTypes.Instance();
        enabled = false;
    }
    public static NetworkManager Instance
    {
        get { return manager; }
    }
    public static void Connection(string ip, int port)
    {
        client = new Client(ip, port);
        client.Connection();
    }



    public static bool IsConnection()
    {
        if (client == null) return false;
        return client.IsConnection();
    }
    public static int GetConnection()
    {
        return client.GetConnection();
    }

    void Update()
    {

        NetworkWriter p = client.GetPack();
        if (p != null)
        {
            registered_types[p.GetTypePack()](p);
        }
    }
    public static void Send(NetworkWriter net_writer)
    {

        client.SendWriter(net_writer);
    }
    private void OnDestroy()
    {
        print("Close socket");
        if (client != null) client.Close();
    }
}
