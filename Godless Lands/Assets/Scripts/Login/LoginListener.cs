using Network.Core;
using Protocol;
using Protocol.Data;
using Protocol.MSG.Login;
using RUCP;
using UnityEngine;
using Zenject;

public class LoginListener : MonoBehaviour {
    private NetworkManager networkManager;
    private LoginInformationWindow loginInformation;
    private ZenjectSceneLoader m_sceneLoader;

    [Inject]
    private void Construct(NetworkManager networkManager, LoginInformationWindow loginInformation, ZenjectSceneLoader sceneLoader)
    {
        this.networkManager = networkManager;
        this.loginInformation = loginInformation;
        m_sceneLoader = sceneLoader;

        networkManager.RegisterHandler(Opcode.MSG_AUTHORIZATION_Response, AuthorizationResponse);
        networkManager.RegisterHandler(Opcode.MSG_REGISTRATION_Response, RegistrationResponse);
    }

    private void AuthorizationResponse(Packet packet)
    {
        packet.Read(out MSG_AUTHORIZATION_SC response);

        if(response.Notification == LoginInformationCode.AuthorizationSuccessful)
        {
            networkManager.LoginID = response.LoginID;
            networkManager.Sessionkey = response.SessionKey;
            m_sceneLoader.LoadSceneAsync("ServersList");
        }
        else loginInformation.ShowInfo(response.Notification);
    }

    private void RegistrationResponse(Packet packet)
    {
        packet.Read(out MSG_REGISTRATION_SC response);

        loginInformation.ShowInfo(response.Notification);
    }


    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Opcode.MSG_AUTHORIZATION_Response);
        networkManager?.UnregisterHandler(Opcode.MSG_REGISTRATION_Response);
    }
}
