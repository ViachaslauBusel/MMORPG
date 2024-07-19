using Cysharp.Threading.Tasks;
using Network.Core;
using Protocol.Data;
using Protocol.MSG.Login;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoginConnection : MonoBehaviour {
    private const int LOGIN_SERVER_PORT = 3737;
    [SerializeField] string loginServerIP = "127.0.0.1";
    [SerializeField] bool localhost = false;
    [SerializeField] short version = 5;

    private InputField input_login;
    private InputField input_pass;
    private NetworkManager m_networkManager;
    private LoginInformationWindow m_informationWindow;

    [Inject]
    private void Construct(NetworkManager networkManager, LoginInformationWindow loginInformation)
    {
        this.m_networkManager = networkManager;
        this.m_informationWindow = loginInformation;
    }
    private void Awake()
    {
        if (localhost) loginServerIP = "127.0.0.1";
        input_login = GameObject.Find("InputFieldLogin").GetComponent<InputField>();
        input_pass = GameObject.Find("InputFieldPassword").GetComponent<InputField>();
    }



    public async void AuthorizationRequest()
    {
        m_informationWindow.Wait("connection waiting");
        bool connected = await ConnectToLoginServer();
        if (connected)
        {
            if (input_login.text.Length > 30 || input_login.text.Length < 3) { m_informationWindow.ShowInfo(LoginInformationCode.WrongLogin); return; }

            if (input_pass.text.Length > 30 || input_pass.text.Length < 3) { m_informationWindow.ShowInfo(LoginInformationCode.WrongPassword); return; }

            string hash_pass = MD5Crypto.GetMd5Hash(input_pass.text);

            MSG_AUTHORIZATION_CS request = new MSG_AUTHORIZATION_CS();
            request.Version = version;
            request.Login = input_login.text;
            request.Password = hash_pass;
            m_networkManager.Client.Send(request);
        }
    }
    public async void RegistrationRequest()
    {
        m_informationWindow.Wait("connection waiting");
        bool connected = await ConnectToLoginServer();
        if (connected)
        {
            if (input_login.text.Length > 30 || input_login.text.Length < 3) { m_informationWindow.ShowInfo(LoginInformationCode.WrongLogin); return; }

            if (input_pass.text.Length > 30 || input_pass.text.Length < 3) { m_informationWindow.ShowInfo(LoginInformationCode.WrongPassword); return; }

            
            string hash_pass = MD5Crypto.GetMd5Hash(input_pass.text);

            MSG_REGISTRATION_CS request = new MSG_REGISTRATION_CS();
            request.Version = version;
            request.Login = input_login.text;
            request.Password = hash_pass;
            m_networkManager.Client.Send(request);
        }
    }

    private async UniTask<bool> ConnectToLoginServer()
    {
        if (!m_networkManager.IsConnectedTo(loginServerIP, LOGIN_SERVER_PORT))
        {
            bool result = await m_networkManager.ConnectTo(loginServerIP, LOGIN_SERVER_PORT);

            if(!result){ m_informationWindow.ShowInfo(LoginInformationCode.ConnectionFail); }

            return result;
        }
        return true;
    }
   

    public void Exit()
    {
        Application.Quit();
    }
}
