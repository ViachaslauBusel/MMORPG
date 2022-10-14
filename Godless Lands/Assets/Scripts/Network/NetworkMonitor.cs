using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NetworkMonitor : MonoBehaviour {

    private Text client_txt;
    private float deltaTime = 0.0f;
    private NetworkManager networkManager;

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
            string text = string.Format("{1:0.} ({0:0.0} ms)", fps, msec);
            client_txt.text = "FPS: "+text+'\n';
            client_txt.text += "Ping: ";
            client_txt.text += networkManager.Client.Statistic.Ping.ToString();
            client_txt.text += "ms\n";

            client_txt.text += "Lost[";
            client_txt.text += networkManager.Client.Statistic.ResentPackets.ToString();
            client_txt.text += '/';
            client_txt.text += networkManager.Client.Statistic.SentPackets.ToString();
            client_txt.text += ']';
        }
        else
        {
            client_txt.text = "connection error";
        }
    }
}
