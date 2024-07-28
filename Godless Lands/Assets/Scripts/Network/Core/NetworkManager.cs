using Cysharp.Threading.Tasks;
using RUCP;
using RUCP.Channels;
using System;
using UnityEngine;
using UnityDebug = UnityEngine.Debug;

namespace Network.Core
{
    public class NetworkManager : MonoBehaviour
    {

        public static NetworkManager Instance { get; private set; }

        public int Sessionkey { get; set; }
        public int LoginID { get; set; }


        public Client Client { get; private set; }
        public UnityProfile Profile { get; } = new UnityProfile();


        public bool isConnected => (Client != null) && Client.Status == NetworkStatus.CONNECTED;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

        }
        internal bool IsConnectedTo(string ip, int port)
        {
            if (!isConnected) return false;

            return Client.RemoteAddress.Address.ToString().Equals(ip) && Client.RemoteAddress.Port == port;
        }
        public void RegisterHandler(short type, Action<Packet> method)
        {
            Profile.Handlers.RegisterHandler(type, method);
        }
        public void UnregisterHandler(short type)
        {
            Profile.Handlers.UnregisterHandler(type);
        }
        public async UniTask<bool> ConnectTo(string ip, int port)
        {
            Client?.Close();
            Client = new Client();
            Client.SetHandler(() => Profile);
            Client.ConnectTo(ip, port);

            await UniTask.WaitWhile(() => Client.Status == NetworkStatus.LISTENING);

            return Client.Status == NetworkStatus.CONNECTED;
        }
        public void ConnectTo(Client client)
        {
            Client?.Close();
            Client = client;
        }

        void Update()
        {
            Profile.ProcessPacket(10);
        }
        public bool Send(Packet net_writer)
        {
            try
            {
                Client.Send(net_writer);
                return true;
            }
            catch (BufferOverflowException e)
            {
                UnityDebug.LogError(e);
            }
            return false;
        }
        private void OnDestroy()
        {
            print("Close socket");
            Client?.Close();
        }
    }
}