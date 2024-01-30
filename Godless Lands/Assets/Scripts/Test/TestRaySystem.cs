using Helpers;
using Protocol;
using Protocol.MSG.Game.ToClient;
using Protocol.MSG.Game.ToServer;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Test
{
    internal class TestRaySystem : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private List<Vector3> _points = new List<Vector3>();

        [Inject]
        public void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
            _networkManager.RegisterHandler(Opcode.MSG_RAYCAST_TEST, OnRaycastTest);
        }

        private void OnRaycastTest(Packet packet)
        {
           packet.Read(out MSG_RAYCAST_TEST_SC msg);

            _points.Clear();
            Debug.Log($"Raycast result count: {msg.RaycastReslut.Count}");
            foreach (var point in msg.RaycastReslut)
            {
                _points.Add(point.ToUnity());
            }
        }

        public void Update()
        {
            if (Input.GetButton("Jump"))
            {
                MSG_RAYCAST_TEST_CS msg = new MSG_RAYCAST_TEST_CS();
                _networkManager.Client.Send(msg);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var point in _points)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_RAYCAST_TEST);
        }
    }
}
