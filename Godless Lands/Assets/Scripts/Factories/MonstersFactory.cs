using AssetPerformanceToolkit.AssetManagement;
using Cysharp.Threading.Tasks;
using Protocol.Data.Units;
using Units.Monster;
using Units.Registry;
using Zenject;

namespace Factories
{
    public class MonstersFactory : AddressablesAssetFactory
    {
        private MonstersRegistry _monstersRegistry;
        private DiContainer _diContainer;

        public MonstersFactory(DiContainer diContainer, MonstersRegistry monstersRegistry) : base(diContainer)
        {
            _diContainer = diContainer;
            _monstersRegistry = monstersRegistry;
        }

        public async UniTask<AssetInstance> CreateMonsterAsync(int skinID)
        {
            var assetInstance = await LoasAssetInstanceAssync(_monstersRegistry, skinID);
            assetInstance.InstanceObject.GetComponent<UnitVisualObject>()?.Initialize(skinID, UnitType.Monster);
            return assetInstance;
        }
    }
}
