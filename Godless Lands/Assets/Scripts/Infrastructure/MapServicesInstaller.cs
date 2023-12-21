using MCamera;
using Physic.Dynamic;
using Services.Replication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapServicesInstaller : MonoInstaller
{
    [SerializeField] ReplicationService m_replicationServiceObj;
    [SerializeField] CharactersFactory m_charactersFactoryObj;
    [SerializeField] DynamicObjectControllersFactory m_objectControllersFactoryObj;
    public override void InstallBindings()
    {

        Container.Bind<ReplicationService>().FromInstance(m_replicationServiceObj).AsSingle();
        Container.Bind<CharactersFactory>().FromInstance(m_charactersFactoryObj).AsSingle();
        Container.Bind<DataHandlerStorage>().AsSingle().NonLazy();
        Container.Bind<DynamicObjectControllersFactory>().FromInstance(m_objectControllersFactoryObj).AsSingle();
    }
}
