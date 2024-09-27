using Protocol.Data.Units;
using Units.Registry;
using UnityEngine;
using Zenject;


namespace Factories
{
    public class CharactersFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _humanMalePrefab;
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        public GameObject CreateHumanMale(Transform parent, Vector3 position)
        {
            return _container.InstantiatePrefab(_humanMalePrefab, position, Quaternion.identity, parent);
        }
    }
}