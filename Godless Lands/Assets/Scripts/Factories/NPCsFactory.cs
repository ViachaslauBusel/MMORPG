using AssetPerformanceToolkit.AssetManagement;
using Cysharp.Threading.Tasks;
using Network.Object.Interaction;
using NPCs;
using Protocol.Data.Units;
using Units.Registry;
using Zenject;

namespace Factories
{
    internal class NPCsFactory : AddressablesAssetFactory
    {
        private NPCsRegistry _npcsRegistry;
        private DiContainer _diContainer;

        public NPCsFactory(DiContainer diContainer, NPCsRegistry npcsRegistry) : base(diContainer)
        {
            _npcsRegistry = npcsRegistry;
            _diContainer = diContainer;
        }

        public async UniTask<AssetInstance> CreateNPC(int skinID)
        {
            var assetInstance = await LoasAssetInstanceAssync(_npcsRegistry, skinID);
            assetInstance.InstanceObject.GetComponent<UnitVisualObject>()?.Initialize(skinID, UnitType.Monster);
            assetInstance.InstanceObject.GetComponent<NPCDialogueInteractionObject>()?.Init(_npcsRegistry.GetObjectByID(skinID).Dialog);
            return assetInstance;
        }
    }
}
