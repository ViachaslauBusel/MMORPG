using Equipment;
using Inventory;
using Items;
using MCamera;
using NetworkObjectVisualization.Characters;
using ObjectInteraction;
using OpenWorld;
using Player;
using Skills;
using UnitVisualCache;
using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    [SerializeField] CameraController m_cameraControllerObj;
    [SerializeField] MapLoader m_mapLoaderObj;
    [SerializeField] TargetView m_targetViewObj;
    [SerializeField] SkillsBook m_skillsBookObj;
    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(m_cameraControllerObj).AsSingle();
        Container.Bind<MapLoader>().FromInstance(m_mapLoaderObj).AsSingle();
        Container.Bind<TargetView>().FromInstance(m_targetViewObj).AsSingle();
        Container.Bind<SkillsBook>().FromInstance(m_skillsBookObj).AsSingle();


        Container.Bind<InteractableObjectsRegistry>().FromNew().AsSingle();
        Container.Bind<InteractionObjectInputHandler>().AsSingle().NonLazy();
        Container.Bind<PlayerCharacterNetworkObjecEventNotifier>().AsSingle().NonLazy();
        Container.Bind<PlayerCharacterVisualEventNotifier>().AsSingle().NonLazy();
       

        Container.Bind<InventoryModel>().AsSingle().NonLazy();
        Container.Bind<InventoryListener>().AsSingle().NonLazy();
        Container.Bind<ItemUsageService>().AsSingle().NonLazy();

        //Equipment
        Container.Bind<EquipmentListener>().AsSingle().NonLazy();
        Container.Bind<EquipmentModel>().AsSingle().NonLazy();

        // Unit visualisation
        Container.BindInterfacesAndSelfTo<UnitVisualCacheService>().AsSingle().NonLazy();
        Container.Bind<CharacterMeshProviderService>().AsSingle().NonLazy();
    }
}