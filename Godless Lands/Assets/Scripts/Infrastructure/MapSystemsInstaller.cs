using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systems.Stats;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class MapSystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private CharacterStatsHolder _characterStatsHolder;

        public override void InstallBindings()
        {
            Container.Bind<CharacterStatsHolder>().FromInstance(_characterStatsHolder).AsSingle();
        }
    }
}
