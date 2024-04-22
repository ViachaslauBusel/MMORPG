using RUCP;
using UI.ConfirmationDialog;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ExitGame : MonoBehaviour
{
    private NetworkManager _networkManager;
    private ConfirmationDialogController _confirmationDialog;

    [Inject]
    private void Construct(NetworkManager networkManager, ConfirmationDialogController confirmationDialog)
    {
        _networkManager = networkManager;
        _confirmationDialog = confirmationDialog;

        //networkManager.RegisterHandler(Types.LobbyReload, LobbyReload);
    }


    private void LobbyReload(Packet nw)//Успешный выход в лобби на сервере
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnExit()
    {
        _confirmationDialog.AddRequest(
        "Вы действительно хотите выйти из игры?",
        () => {
            Application.Quit();
        },
        () => { }
        );
    }
    public void OnReload()
    {
        _confirmationDialog.AddRequest(
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
        //_networkManager?.UnregisterHandler(Types.LobbyReload);
    }
}
