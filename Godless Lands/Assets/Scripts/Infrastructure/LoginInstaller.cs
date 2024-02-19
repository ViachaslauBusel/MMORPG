using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoginInstaller : MonoInstaller
{
    [SerializeField] LoginInformationWindow m_informationWindow;
    public override void InstallBindings()
    {

        Container.Bind<LoginInformationWindow>().FromInstance(m_informationWindow).AsSingle();
    }
}
