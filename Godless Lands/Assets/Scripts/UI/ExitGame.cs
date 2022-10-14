using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ExitGame : MonoBehaviour {
    private NetworkManager networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.LobbyReload, LobbyReload);
    }


    private void LobbyReload(Packet nw)//Успешный выход в лобби на сервере
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnExit()
    {
        Confirm.Instance.Subscribe(
        "Вы действительно хотите выйти из игры?",
        () => {
            Application.Quit();
        },
        () => { }
        );
    }
    public void OnReload()
    {
        Confirm.Instance.Subscribe(
        "Вы действительно хотите выйти в комнату выбора персонажа?",
        () => {
        //TODO msg
            //Packet nw = new Packet(Channel.Reliable);
            //nw.WriteType(Types.LobbyReload);
            //NetworkManager.Send(nw);
        },
        () => { }
        );
    }


    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.LobbyReload);
    }
}
