using Cysharp.Threading.Tasks;
using Network.Object.Visualization.Entities.Characters;
using Units.Resource.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Factories
{
    internal class MiningStonesFactory : AddressablesAssetFactory
    {
        [SerializeField]
        private ResourcesRegistry _resourcesData;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async UniTask<AssetHolder> CreateStone(int skinID) => await CreateAssetHolder(_resourcesData, skinID);
    }
}
