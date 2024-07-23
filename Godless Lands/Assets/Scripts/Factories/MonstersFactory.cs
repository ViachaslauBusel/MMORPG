using UnityEngine;
using Units.Monster;
using Zenject;
using Cysharp.Threading.Tasks;
using Network.Object.Visualization.Entities.Characters;

namespace Factories
{
    public class MonstersFactory : AddressablesAssetFactory
    {
        [SerializeField]
        private MonstersRegistry _monstersRegistry;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public async UniTask<AssetHolder> CreateMonster(int skinID) => await CreateAssetHolder(_monstersRegistry, skinID);
    }
}
