using RUCP.Handler;
using RUCP.Packets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginListener : MonoBehaviour {

    void Awake()
    {
        HandlersStorage.RegisterHandler(Types.LoginInfo, LoginInfo);
        HandlersStorage.RegisterHandler(Types.LoginOk, LoginOk);
    }

    private void LoginOk(Packet nw)
    {
        NetworkManager.LoginID = nw.ReadInt();
        NetworkManager.Sessionkey = nw.ReadInt();
        SceneManager.LoadScene("ServersList");
    }

    private void LoginInfo(Packet nw)
    {
        int code = nw.ReadByte();
        LoginInformation.ShowInfo(code);
    }


    private void OnDestroy()
    {

        HandlersStorage.UnregisterHandler(Types.LoginInfo);
        HandlersStorage.UnregisterHandler(Types.LoginOk);
    }
}
