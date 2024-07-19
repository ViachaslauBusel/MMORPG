using UnityEngine;
using Walkers.Monster;
using Zenject;

namespace Factories
{
    public class MonstersFactory : MonoBehaviour
    {
        [SerializeField]
        private MonstersRegistry _monstersRegistry;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateMonster(int skinID, Transform transform, Vector3 position)
        {
           var monsterData = _monstersRegistry.GetObjectByID(skinID);

            if(monsterData == null)
            {
                Debug.LogError($"Monster with id {skinID} not found");
                return null;
            }

            return _diContainer.InstantiatePrefab(monsterData.Prefab.editorAsset, position, Quaternion.identity, transform);
        }
    }
}
