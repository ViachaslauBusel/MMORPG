using MCamera;
using Services.Replication;
using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    [SerializeField] CameraController m_cameraControllerObj;
    public override void InstallBindings()
    {

        Container.Bind<CameraController>().FromInstance(m_cameraControllerObj).AsSingle();
    }
}