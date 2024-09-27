using AssetPerformanceToolkit.AssetManagement;
using Cysharp.Threading.Tasks;
using Protocol.Data.Units;
using Units.Registry;
using Units.Resource.Data;
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

        public async UniTask<AssetInstance> CreateStone(int skinID)
        {
            var assetInstance = await LoasAssetInstanceAssync(_resourcesData, skinID);
            assetInstance.InstanceObject.GetComponent<UnitVisualObject>()?.Initialize(skinID, UnitType.Resource);
            return assetInstance;
        }
    }
}
