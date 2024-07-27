using Cysharp.Threading.Tasks;
using Network.Object.Visualization.Entities.Characters;
using NPCs;
using Units.Monster;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

        public async UniTask<AssetHolder> CreateNPC(int skinID) => await CreateAssetHolder(_npcsRegistry, skinID);
    }
}
