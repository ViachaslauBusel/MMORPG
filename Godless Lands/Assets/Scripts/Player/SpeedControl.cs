using RUCP.Handler;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SpeedControl : MonoBehaviour
    {
        private void Awake()
        {
            HandlersStorage.RegisterHandler(Types.MoveCorrection, MoveCorrection);
        }
        private void MoveCorrection(Packet packet)
        {
            Vector3 cast = packet.ReadVector3();
            transform.position += cast;
            print($"cast: {cast}");
        }
        private void OnDestroy()
        {
            HandlersStorage.UnregisterHandler(Types.MoveCorrection);
        }
    }
}