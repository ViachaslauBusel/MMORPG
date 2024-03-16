using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using Items;
using Walkers.Monsters;
using Physic.Dynamic;

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
        private DynamicObjectControllersFactory _dynamicObjectControllersFactory;

        public override void InstallBindings()
        {
            Container.Bind<ItemsFactory>().FromInstance(_itemsFactory).AsSingle();
            Container.Bind<CharactersFactory>().FromInstance(_charactersFactory).AsSingle();
            Container.Bind<MonstersFactory>().FromInstance(_monstersFactory).AsSingle();
            Container.Bind<DynamicObjectControllersFactory>().FromInstance(_dynamicObjectControllersFactory).AsSingle();
        }
    }
}
