﻿using Animation;
using Factories;
using Items;
using Network.Object.Dynamic;
using Quests.Data;
using Recipes;
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
        private CraftingStationsFactory _workbenchesFactory;
        [SerializeField]
        private DynamicObjectControllersFactory _dynamicObjectControllersFactory;
        [SerializeField]
        private NPCsFactory _npcsFactory;
        [SerializeField]
        private QuestDataHolder _questsDataHolder;
        [SerializeField]
        private DropBagFactory _dropBagFactory;
        [SerializeField]
        private AnimationPriorityDataHolder _animationPriorityDataHolder;

        public override void InstallBindings()
        {
            Container.Bind<ItemsFactory>().FromInstance(_itemsFactory).AsSingle();
            Container.Bind<CharactersFactory>().FromInstance(_charactersFactory).AsSingle();
            Container.Bind<MonstersFactory>().FromInstance(_monstersFactory).AsSingle();
            Container.Bind<MiningStonesFactory>().FromInstance(_miningStonesFactory).AsSingle();
            Container.Bind<CraftingStationsFactory>().FromInstance(_workbenchesFactory).AsSingle();
            Container.Bind<DynamicObjectControllersFactory>().FromInstance(_dynamicObjectControllersFactory).AsSingle();
            Container.Bind<NPCsFactory>().FromInstance(_npcsFactory).AsSingle();
            Container.Bind<QuestDataHolder>().FromInstance(_questsDataHolder).AsSingle();
            Container.Bind<DropBagFactory>().FromInstance(_dropBagFactory).AsSingle();
            Container.Bind<AnimationPriorityDataHolder>().FromInstance(_animationPriorityDataHolder).AsSingle();
            Container.Bind<RecipesDataHolder>().AsSingle();
        }
    }
}
