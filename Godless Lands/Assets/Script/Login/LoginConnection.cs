using RUCP.Network;
using RUCP.Packets;
using System.Collections;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class LoginConnection : MonoBehaviour {
    public string loginServer = "127.0.0.1";
    public bool localhost = false;
    public short version = 5;

    private InputField input_login;
    private InputField input_pass;

    private void Awake()
    {
        if (localhost) loginServer = "127.0.0.1";
        input_login = GameObject.Find("InputFieldLogin").GetComponent<InputField>();
        input_pass = GameObject.Find("InputFieldPassword").GetComponent<InputField>();
    }

    public void Connectionlogin()
    {
        if (!NetworkManager.isConnected) StartCoroutine(IEConnectionServer(Types.Login));
        else SendLoginOrReg(Types.Login);
    }
    public void ConnectionReg()
    {
        if (!NetworkManager.isConnected) StartCoroutine(IEConnectionServer(Types.Registration));
        else SendLoginOrReg(Types.Registration);
    }


    private IEnumerator IEConnectionServer(short types)
    {
        print("Cоеденение с сервером");
        NetworkManager.Connection(loginServer, 3737);
   
        while (NetworkManager.Socket.NetworkStatus == NetworkStatus.LISTENING)
        {
            yield return null;
        }
        if (NetworkManager.isConnected) //Если удалось соедениться с сервером
        {
            SendLoginOrReg(types);//Отпровляем данные на логин или регистрацию
        }
        else
        {
            LoginInformation.ShowInfo(1);
        }
    }

    private void SendLoginOrReg(short types)
    {
   
        if (input_login.text.Length > 30 || input_login.text.Length < 3) { LoginInformation.ShowInfo(2); return; }

        if (input_pass.text.Length > 30 || input_pass.text.Length < 3) { LoginInformation.ShowInfo(3); return; }

        string hash_pass = MD5Crypto.GetMd5Hash(input_pass.text);

        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(types);
        nw.WriteShort(version);
        nw.WriteString(input_login.text);
        nw.WriteString(hash_pass);
       
        NetworkManager.Send(nw);
    }
   

    public void Exit()
    {
        Application.Quit();
    }
}
