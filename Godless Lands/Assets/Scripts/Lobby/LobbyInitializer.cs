using RUCP.Network;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyInitializer : MonoBehaviour
{
   
    void Start()
    {
        ConnectionLobby();
    }

    private void ConnectionLobby()
    {
        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.LobbyEntrance);
        NetworkManager.Send(nw);
    }
}
