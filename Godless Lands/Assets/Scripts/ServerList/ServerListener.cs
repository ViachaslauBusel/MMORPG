using Protocol;
using Protocol.MSG.Game;
using Protocol.MSG.Login;
using RUCP;
using RUCP.Handler;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ServerList
{
    public class ServerListener : MonoBehaviour
    {

        [SerializeField] Dropdown dropdown;
        private List<string> ip;
        private List<int> port;
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
            networkManager.RegisterHandler(Opcode.MSG_SERVER_LIST, ServersList);
        }

        void Start()
        {
            ip = new List<string>();
            port = new List<int>();
           
            GetServersList();
        }


        private void GetServersList()
        {
           MSG_SERVER_LIST_CS request = new MSG_SERVER_LIST_CS();
           networkManager.Client.Send(request);
        }


        private void ServersList(Packet packet)
        {
            //  print(nw.AvailableBytes);
            dropdown.ClearOptions();
            packet.Read(out MSG_SERVER_LIST_SC response);
            List<string> names = new List<string>();
            foreach(var server in response.Servers)
            {
                names.Add(server.Name);
                ip.Add(server.IP);
                port.Add(server.Port);

            }
            // foreach (string n in names) print(n);
          
            dropdown.AddOptions(names);

            if (ip.Count > 0) SL_Main.Instance.SetAdress(ip[0], port[0]);

        }

        public void SetServer(int i)
        {
            SL_Main.Instance.SetAdress(ip[dropdown.value], port[dropdown.value]);
        }

        private void OnDestroy()
        {
            networkManager?.UnregisterHandler(Types.ServersList);
        }
    }
}
