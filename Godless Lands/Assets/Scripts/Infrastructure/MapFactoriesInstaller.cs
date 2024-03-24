using Factories;
using Items;
using Physic.Dynamic;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    internal class MapFactoriesInstaller : MonoInstaller
    {
        [SerializeField]
        private ItemsFactory _itemsFactory;
        [SerializeField]
        private CharactersFactory _charactersFactory;
        [SerializeField]
        private MonstersFactory _monstersFactory;
        [SerializeField]
        private MiningStonesFactory _miningStonesFactory;
        [SerializeField]
        private DynamicObjectControllersFactory _dynamicObjectControllersFactory;

        public override void InstallBindings()
        {
            Container.Bind<ItemsFactory>().FromInstance(_itemsFactory).AsSingle();
            Container.Bind<CharactersFactory>().FromInstance(_charactersFactory).AsSingle();
            Container.Bind<MonstersFactory>().FromInstance(_monstersFactory).AsSingle();
            Container.Bind<MiningStonesFactory>().FromInstance(_miningStonesFactory).AsSingle();
            Container.Bind<DynamicObjectControllersFactory>().FromInstance(_dynamicObjectControllersFactory).AsSingle();
        }
    }
}
