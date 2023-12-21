using RUCP;
using RUCP.Handler;
using UnityEngine;
using Zenject;

namespace Player
{
    public class SpeedControl : MonoBehaviour
    {
        private NetworkManager networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
            networkManager.RegisterHandler(Types.MoveCorrection, MoveCorrection);
        }

        private void MoveCorrection(Packet packet)
        {
            Vector3 cast = packet.ReadVector3();
            transform.position += cast;
            print($"cast: {cast}");
        }
        private void OnDestroy()
        {
            networkManager?.UnregisterHandler(Types.MoveCorrection);
        }
    }
}