using NPCs;
using UnityEngine;
using Zenject;

namespace Factories
{
    internal class NPCsFactory : MonoBehaviour
    {
        [SerializeField]
        private NPCsRegistry _npcsRegistry;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateNPC(int skinID, Transform transform, Vector3 position, float rotation)
        {
            var npcData = _npcsRegistry.GetObjectByID(skinID);

            if (npcData == null)
            {
                Debug.LogError($"Monster with id {skinID} not found");
                return null;
            }

            return _diContainer.InstantiatePrefab(npcData.Prefab.editorAsset, position, Quaternion.Euler(0, rotation, 0), transform);
        }
    }
}
