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
        [SerializeField]
        private NPCsRegistry _npcsRegistry;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async UniTask<AssetHolder> CreateNPC(int skinID) => await CreateAssetHolder(_npcsRegistry, skinID);
    }
}
