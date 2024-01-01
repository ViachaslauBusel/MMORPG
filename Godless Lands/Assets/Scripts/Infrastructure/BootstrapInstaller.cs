using Loader;
using Services.Replication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] GameObject m_networkManagerPrefab;
    [SerializeField] GameObject m_loadingScreenPrefab;
    public override void InstallBindings()
    {
        NetworkManager networkManager = Container.InstantiatePrefabForComponent<NetworkManager>(m_networkManagerPrefab, Vector3.zero, Quaternion.identity, null);
       
        Container.Bind<NetworkManager>().FromInstance(networkManager).AsSingle();
        Container.Bind<LoadingScreenDisplay>().FromComponentInNewPrefab(m_loadingScreenPrefab).AsSingle().NonLazy();
        Container.Bind<SessionManagmentService>().AsSingle().NonLazy();
        Container.Bind<ServerSettingsProvider>().AsSingle().NonLazy();
        Container.Bind<SceneLoader>().AsSingle().NonLazy();
    }
}
