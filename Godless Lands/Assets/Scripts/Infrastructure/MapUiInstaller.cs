using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using UI.PlayerCharacterDead;
using GameWorldInteractions;

namespace Infrastructure
{
    public class MapUiInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerCharacterDeadWindow _playerCharacterDeadWindow;
        [SerializeField]
        private InteractionIndicator _interactionIndicator;

        public override void InstallBindings()
        {
            Container.Bind<PlayerCharacterDeadWindow>().FromInstance(_playerCharacterDeadWindow).AsSingle();
            Container.Bind<InteractionIndicator>().FromInstance(_interactionIndicator).AsSingle();
        }
    }
}
