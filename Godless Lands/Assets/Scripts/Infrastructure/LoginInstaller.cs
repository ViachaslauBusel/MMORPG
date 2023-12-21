using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoginInstaller : MonoInstaller
{
    [SerializeField] GameObject m_informationWindow;
    public override void InstallBindings()
    {

        Container.Bind<LoginInformationWindow>().FromComponentOn(m_informationWindow).AsSingle();
    }
}
