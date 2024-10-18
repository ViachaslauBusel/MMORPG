
using UnityEngine;

namespace Player.Controller
{
    public class PlayerMovementController
    {
        private CharacterInstanceMovementController _characterInstanceMovementController;
        private byte _segment;

        public void Initialize(byte segment)
        {
            _segment = segment;
            Debug.Log($"PlayerMovementController initialized:{_segment}");
        }

        public void RegisterCharacterInstance(CharacterInstanceMovementController characterInstanceMovementController)
        {
            if(_characterInstanceMovementController != null)
            {
                Debug.LogError("Character instance already registered");
            }
            _characterInstanceMovementController = characterInstanceMovementController;
            _characterInstanceMovementController.Initialize(_segment);
        }

        public void UnregisterCharacterInstance()
        {
            if(_characterInstanceMovementController == null)
            {
                Debug.LogError("Character instance not registered");
            }
            _characterInstanceMovementController = null;
        }
    }
}
