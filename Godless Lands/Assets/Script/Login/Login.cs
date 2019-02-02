
using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {





    void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.LoginInfo, LoginInfo);
        RegisteredTypes.RegisterTypes(Types.LoginOk, LoginOk);
    }

    private void LoginOk(NetworkWriter nw)
    {
        print("login ok");
        NetworkManager.SetLoginID(nw.ReadInt());
        NetworkManager.SetKey(nw.ReadInt());
        SceneManager.LoadScene("ServersList");
    }

    private void LoginInfo(NetworkWriter nw)
    {
        byte code = nw.ReadByte();
        LoginInformation.ShowInfo(code);
      
    }


    private void OnDestroy()
    {

        RegisteredTypes.UnregisterTypes(Types.LoginInfo);
        RegisteredTypes.UnregisterTypes(Types.LoginOk);
    }
}
