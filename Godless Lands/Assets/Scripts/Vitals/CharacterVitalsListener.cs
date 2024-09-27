using Network.Core;
using Protocol;
using Protocol.MSG.Game.ToClient.Stats;
using Protocol.MSG.Game.ToClient.Vitals;
using RUCP;
using System;
using UnityEngine;

namespace Vitals
{
    public class CharacterVitalsListener : IDisposable
    {
        private NetworkManager _networkManager;
        private CharacterVitalsStorage _characterVitalsHolder;

        public CharacterVitalsListener(NetworkManager networkManager, CharacterVitalsStorage characterVitalsHolder)
        {
            _networkManager = networkManager;
            _characterVitalsHolder = characterVitalsHolder;

            _networkManager.RegisterHandler(Opcode.MSG_UPDATE_VITALS, UpdateVitals);
        }

        private void UpdateVitals(Packet packet)
        {
            packet.Read(out MSG_UPDATE_VITALS updVitals);

            //Debug.Log("Vitals updated");

            foreach (var vital in updVitals.Vitals)
            {
                _characterVitalsHolder.UpdateVital(vital.Code, vital.Value, vital.MaxValue);
            }
            _characterVitalsHolder.NotifyVitalsUpdated();
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_UPDATE_STATS);
        }
    }
}
