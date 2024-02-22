using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using UI.PlayerCharacterDead;

namespace Infrastructure
{
    public class MapUiInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerCharacterDeadWindow _playerCharacterDeadWindow;

        public override void InstallBindings()
        {
            Container.Bind<PlayerCharacterDeadWindow>().FromInstance(_playerCharacterDeadWindow).AsSingle();
        }
    }
}
