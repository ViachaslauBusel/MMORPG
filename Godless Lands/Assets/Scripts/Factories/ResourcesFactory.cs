using Cysharp.Threading.Tasks;
using Network.Object.Visualization.Entities.Characters;
using Units.Resource.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Factories
{
    internal class ResourcesFactory : AddressablesAssetFactory
    {
        private ResourcesRegistry _resourcesData;

        public ResourcesFactory(ResourcesRegistry resourcesData, DiContainer diContainer) : base(diContainer)
        {
            _resourcesData = resourcesData;
        }

        public async UniTask<AssetHolder> CreateStone(int skinID) => await CreateAssetHolder(_resourcesData, skinID);
    }
}
