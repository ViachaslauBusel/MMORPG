using Protocol.MSG.Game;
using UnityEngine;
using Zenject;

public class LobbyInitializer : MonoBehaviour
{
    private NetworkManager m_networkManager;

    [Inject]
    private void Consruct(NetworkManager networkManager)
    {
        m_networkManager = networkManager;
    }
    void Start()
    {
        ConnectToLobby();
    }

    private void ConnectToLobby()
    {
       MSG_LOBBY_ENTRANCE_CS request = new MSG_LOBBY_ENTRANCE_CS();
       m_networkManager.Client.Send(request);
    }
}
