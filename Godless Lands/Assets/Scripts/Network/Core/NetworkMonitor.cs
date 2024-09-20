using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Text;

namespace Network.Core
{
    public class NetworkMonitor : MonoBehaviour
    {
        private Text client_txt;
        private float deltaTime = 0.0f;
        private NetworkManager networkManager;
        private StringBuilder stringBuilder = new StringBuilder();

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
        }

        private void Start()
        {
            client_txt = GetComponent<Text>();
        }

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            if (networkManager.isConnected)
            {
                float fps = deltaTime * 1000.0f;
                float msec = 1.0f / deltaTime;

                stringBuilder.Clear();
                stringBuilder.AppendFormat("FPS: {0:0.} ({1:0.0} ms)\n", msec, fps);
                stringBuilder.AppendFormat("Ping: {0}ms\n", networkManager.Client.Statistic.Ping);
                stringBuilder.AppendFormat("Lost[{0}/{1}]", networkManager.Client.Statistic.ResentPackets, networkManager.Client.Statistic.SentPackets);

                client_txt.text = stringBuilder.ToString();
            }
            else
            {
                client_txt.text = "connection error";
            }
        }
    }
}
