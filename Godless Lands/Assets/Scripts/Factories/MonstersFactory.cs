using UnityEngine;
using Units.Monster;
using Zenject;
using Cysharp.Threading.Tasks;
using Network.Object.Visualization.Entities.Characters;

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

        public async UniTask<AssetHolder> CreateMonster(int skinID) => await CreateAssetHolder(_monstersRegistry, skinID);
    }
}
