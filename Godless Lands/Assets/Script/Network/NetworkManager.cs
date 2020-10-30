using RUCP;
using RUCP.BufferChannels;
using RUCP.Debugger;
using RUCP.Handler;
using RUCP.Packets;
using System.Net.Sockets;
using UnityEngine;
using UnityDebug = UnityEngine.Debug;

public class NetworkManager : MonoBehaviour {

    public static NetworkManager Instance { get; private set; }

    public static int Sessionkey { get; set; }
    public static int LoginID { get; set; }

    private static DebugBuffer debugBuffer;


    public static ServerSocket Socket { get; set; }

    public static bool isConnected => (Socket != null) && Socket.IsConnected();

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        RUCP.Debug.Start();
        debugBuffer = RUCP.Debug.Object as DebugBuffer;

        HandlersStorage.RegisterUnknown(Unknown);
        enabled = false;
    }

    public static void Connection(string ip, int port)
    {
        Socket = new ServerSocket(ip, port);
        Socket.Connection();
        Instance.enabled = true;
    }

    void Update()
    {
        string message = debugBuffer?.GetMessage();
        if (!string.IsNullOrEmpty(message))
            UnityDebug.Log(message);

        var error = debugBuffer?.GetError();
        if (!string.IsNullOrEmpty(error.Value.message))
            UnityDebug.LogError(error.Value.className + " : " + error.Value.message + " : " + error.Value.stackTrace);

        Socket.ProcessPacket(10);
    }
    private void Unknown(Packet nw)
    {
        UnityDebug.Log("Тип пакета не распознан: " + nw.ReadType());
    }
    public static bool Send(Packet net_writer)
    {
        try
        {
            Socket.Send(net_writer);
            return true;
        } catch(BufferOverflowException e)
        {
            UnityDebug.LogError(e);
        }
        return false;
    }
    private void OnDestroy()
    {
        print("Close socket");
        Socket?.Close();
    }
}
