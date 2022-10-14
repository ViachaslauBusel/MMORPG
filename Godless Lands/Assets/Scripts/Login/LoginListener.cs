using Protocol;
using Protocol.Data;
using Protocol.MSG.Login;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoginListener : MonoBehaviour {
    private NetworkManager networkManager;
    private LoginInformationWindow loginInformation;

    [Inject]
    private void Construct(NetworkManager networkManager, LoginInformationWindow loginInformation)
    {
        this.networkManager = networkManager;
        this.loginInformation = loginInformation;

        networkManager.RegisterHandler(Opcode.MSG_AUTHORIZATION_Response, AuthorizationResponse);
        networkManager.RegisterHandler(Opcode.MSG_REGISTRATION_Response, RegistrationResponse);
    }

    private void AuthorizationResponse(Packet packet)
    {
        packet.Read(out MSG_AUTHORIZATION_Response response);

        if(response.Notification == LoginInformationCode.AuthorizationSuccessful)
        {
            networkManager.LoginID = response.LoginID;
            networkManager.Sessionkey = response.SessionKey;
            SceneManager.LoadScene("ServersList");
        }
        else loginInformation.ShowInfo(response.Notification);
    }

    private void RegistrationResponse(Packet packet)
    {
        packet.Read(out MSG_REGISTRATION_Response response);

        loginInformation.ShowInfo(response.Notification);
    }


    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Opcode.MSG_AUTHORIZATION_Response);
        networkManager?.UnregisterHandler(Opcode.MSG_REGISTRATION_Response);
    }
}
