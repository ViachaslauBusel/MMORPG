using Resource;
using Units.Resource.Data;
using UnityEngine;
using Zenject;

namespace Factories
{
    internal class MiningStonesFactory : MonoBehaviour
    {
        [SerializeField]
        private ResourcesRegistry _resourcesData;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateStone(int skinID, Transform transform, Vector3 position)
        {
            var miningStoneData = _resourcesData.GetObjectByID(skinID);

            if (miningStoneData == null)
            {
                Debug.LogError($"Stone with id {skinID} not found");
                return null;
            }

            return _diContainer.InstantiatePrefab(miningStoneData.Prefab.editorAsset, position, Quaternion.identity, transform);
        }
    }
}
