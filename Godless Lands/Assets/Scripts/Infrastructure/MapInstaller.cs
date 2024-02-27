using GameWorldInteractions;
using MCamera;
using OpenWorld;
using Player;
using Services.Replication;
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
        Container.Bind<PlayerCharacterNetworkObjecEventNotifier>().AsSingle().NonLazy();
        Container.Bind<PlayerCharacterVisualEventNotifier>().AsSingle().NonLazy();
        Container.Bind<UnitVisualCacheService>().AsSingle().NonLazy();
    }
}