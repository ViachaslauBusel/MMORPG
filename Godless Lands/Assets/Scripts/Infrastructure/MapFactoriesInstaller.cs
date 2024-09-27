using Animation.Data;
using Factories;
using Items;
using Network.Object.Dynamic;
using NPCs;
using Quests;
using Recipes;
using Skills.Data;
using Units.CraftingStatio.Data;
using Units.Monster;
using Units.Resource.Data;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    internal class MapFactoriesInstaller : MonoInstaller
    {
        [SerializeField]
        private AnimationPriorityData _animationPriorityData;
        [SerializeField]
        private CharactersFactory _charactersFactory;
        [SerializeField]
        private CraftingStationsFactory _craftingStationsFactory;
        [SerializeField]
        private DynamicObjectControllersFactory _dynamicObjectControllersFactory;
        [SerializeField]
        private DropBagFactory _dropBagFactory;
        [SerializeField]
        private ItemsRegistry _itemsRegistry;
        [SerializeField]
        private MonstersRegistry _monstersRegistry;
        [SerializeField]
        private NPCsRegistry _npcsRegistry;
        [SerializeField]
        private QuestRegistry _questRegistry;
        [SerializeField]
        private ResourcesRegistry _resourcesRegistry;
        [SerializeField]
        private SkillsRegistry _skillsRegistry;

        public override void InstallBindings()
        {
            Container.Bind<AnimationPriorityData>().FromInstance(_animationPriorityData).AsSingle();
            Container.Bind<DynamicObjectControllersFactory>().FromInstance(_dynamicObjectControllersFactory).AsSingle();
            Container.Bind<DropBagFactory>().FromInstance(_dropBagFactory).AsSingle();
            Container.Bind<ItemsRegistry>().FromInstance(_itemsRegistry).AsSingle();
            Container.Bind<MonstersRegistry>().FromInstance(_monstersRegistry).AsSingle();
            Container.Bind<NPCsRegistry>().FromInstance(_npcsRegistry).AsSingle();
            Container.Bind<QuestRegistry>().FromInstance(_questRegistry).AsSingle();
            Container.Bind<ResourcesRegistry>().FromInstance(_resourcesRegistry).AsSingle();
            Container.Bind<SkillsRegistry>().FromInstance(_skillsRegistry).AsSingle();
            Container.Bind<RecipesDataHolder>().AsSingle();

            Container.Bind<CraftingStationsFactory>().FromInstance(_craftingStationsFactory).AsSingle();
            Container.Bind<CharactersFactory>().FromInstance(_charactersFactory).AsSingle();
            Container.Bind<ItemsFactory>().AsSingle();
            Container.Bind<ResourcesFactory>().AsSingle();
            Container.Bind<MonstersFactory>().AsSingle();
            Container.Bind<NPCsFactory>().AsSingle();

        }
    }
}
