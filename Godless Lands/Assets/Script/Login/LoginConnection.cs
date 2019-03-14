using RUCP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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
        if (!NetworkManager.IsConnection()) StartCoroutine(IEConnectionServer(Types.Login));
        else SendLoginOrReg(Types.Login);
    }
    public void ConnectionReg()
    {
        if (!NetworkManager.IsConnection()) StartCoroutine(IEConnectionServer(Types.Registration));
        else SendLoginOrReg(Types.Registration);
    }


    private IEnumerator IEConnectionServer(short types)
    {
        NetworkManager.Connection(loginServer, 3737);
        while (NetworkManager.GetConnection() == -1)
        {
            yield return null;
        }
        if (NetworkManager.IsConnection()) //Если удалось соедениться с сервером
        {
            NetworkManager.Instance.enabled = true;
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
       // byte[] _pass;//= Encoding.ASCII.GetBytes(input_pass.text);
        if (input_pass.text.Length > 30 || input_pass.text.Length < 3) { LoginInformation.ShowInfo(3); return; }

        string hash_pass;
        using (MD5 md5Hash = MD5.Create())
        {
            hash_pass = GetMd5Hash(md5Hash, input_pass.text);
        }



        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.write(version);
        nw.write(input_login.text);
        nw.write(hash_pass);
        nw.SetTypePack(types);
        NetworkManager.Send(nw);
    }
    static string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
