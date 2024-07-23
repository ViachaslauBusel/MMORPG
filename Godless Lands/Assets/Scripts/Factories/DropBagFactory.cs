using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Factories
{
    internal class DropBagFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dropBagPrefab;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateDropBag(DropBagType dropBagType, Transform transform, Vector3 position)
        {

            GameObject gameObject = _diContainer.InstantiatePrefab(_dropBagPrefab, position, Quaternion.identity, transform);
            gameObject.transform.localScale = dropBagType switch
            {
                DropBagType.Small => new Vector3(0.5f, 0.5f, 0.5f),
                DropBagType.Medium => new Vector3(0.75f, 0.75f, 0.75f),
                DropBagType.Large => new Vector3(1f, 1f, 1f), 
                _ => Vector3.one 
            };

            return gameObject;
        }
    }
}
