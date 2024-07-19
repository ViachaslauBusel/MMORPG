using UnityEngine;
using Zenject;

namespace Network.Core
{
    public class Ping : MonoBehaviour
    {

        private string ping_txt;
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
        }

        // Update is called once per frame
        void Update()
        {
            ping_txt = networkManager.Client.Statistic.Ping.ToString();
        }

        void OnGUI()
        {
            GUI.Box(new Rect(0, 0, 100, 20), ping_txt + " ms");
        }
    }
}
