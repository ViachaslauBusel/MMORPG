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
            networkManager.RegisterHandler(Types.ServersList, ServersList);
        }

        void Start()
        {
            ip = new List<string>();
            port = new List<int>();
           
            GetServersList();
        }


        private void GetServersList()
        {
            // print("Get list");
            //TODO MSg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.ServersList);
            //NetworkManager.Send(nw);
        }


        private void ServersList(Packet nw)
        {
            //  print(nw.AvailableBytes);
            List<string> names = new List<string>(10);
            while (nw.AvailableBytesForReading > 0)
            {
                //  print("create");


                names.Add(nw.ReadString());
                ip.Add(nw.ReadString());
                port.Add(nw.ReadInt());

            }
            // foreach (string n in names) print(n);
            dropdown.ClearOptions();
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
