using Services.Replication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] GameObject networkManagerPrefab;
    public override void InstallBindings()
    {
        NetworkManager networkManager = Container.InstantiatePrefabForComponent<NetworkManager>(networkManagerPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<NetworkManager>().FromInstance(networkManager).AsSingle();
        Container.Bind<SessionManagmentService>().AsSingle().NonLazy();
        Container.Bind<ServerSettingsProvider>().AsSingle().NonLazy();
    }
}
