using Helpers;
using Player;
using Protocol.MSG.Game.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Skills
{
    public class SkillUsageRequestSender : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private PlayerCharacterVisualEventNotifier _playerCharacterVisualEventNotifier;
        private GameObject _playerCharacterVisual;

        [Inject]
        public void Construct(NetworkManager networkManager, PlayerCharacterVisualEventNotifier playerCharacterVisualEventNotifier)
        {
            _networkManager = networkManager;
            _playerCharacterVisualEventNotifier = playerCharacterVisualEventNotifier;
        }
        private void Awake()
        {
            _playerCharacterVisualEventNotifier.OnPlayerCharacterVisualSpawned += OnPlayerCharacterVisualSpawned;
            _playerCharacterVisualEventNotifier.OnPlayerCharacterVisualDestroyed += OnPlayerCharacterVisualDespawned;
        }

        private void OnPlayerCharacterVisualDespawned(GameObject @object)
        {
            _playerCharacterVisual = null;
        }

        private void OnPlayerCharacterVisualSpawned(GameObject @object)
        {
            _playerCharacterVisual = @object;
        }

        public void SendSkillUsageRequest(int skillID)
        {
            if(_playerCharacterVisual == null)
            {
                return;
            }

            MSG_SKILL_USE_CS request = new MSG_SKILL_USE_CS();
            request.SkillID = skillID;
            request.Position = _playerCharacterVisual.transform.position.ToNumeric();
            _networkManager.Client.Send(request);
        }

        private void OnDestroy()
        {
            _playerCharacterVisualEventNotifier.OnPlayerCharacterVisualSpawned -= OnPlayerCharacterVisualSpawned;
            _playerCharacterVisualEventNotifier.OnPlayerCharacterVisualDestroyed -= OnPlayerCharacterVisualDespawned;
        }
    }
}
