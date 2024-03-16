using Services.Replication;
using UnityEngine;
using Zenject;

public class MapServicesInstaller : MonoInstaller
{
    [SerializeField] ReplicationService m_replicationServiceObj;
    public override void InstallBindings()
    {
        Container.Bind<ReplicationService>().FromInstance(m_replicationServiceObj).AsSingle();
        Container.Bind<DataHandlerStorage>().AsSingle().NonLazy();
    }
}
