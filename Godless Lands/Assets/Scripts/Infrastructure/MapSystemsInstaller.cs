using Cells;
using Drop;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systems.Stats;
using UnityEngine;
using Vitals;
using Zenject;

namespace Infrastructure
{
    public class MapSystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private CharacterStatsHolder _characterStatsHolder;
        [SerializeField]
        private PlayerSkillsHolder _playerSkillsHolder;
        [SerializeField]
        private SkillUsageRequestSender _skillUsageRequestSender;


        public override void InstallBindings()
        {
            Container.Bind<CharacterStatsHolder>().FromInstance(_characterStatsHolder).AsSingle();
            Container.Bind<PlayerSkillsHolder>().FromInstance(_playerSkillsHolder).AsSingle();
            Container.Bind<SkillUsageRequestSender>().FromInstance(_skillUsageRequestSender).AsSingle().NonLazy();

            Container.Bind<DropListener>().FromNew().AsSingle().NonLazy();

            //Vitals
            Container.Bind<CharacterVitalsListener>().FromNew().AsSingle().NonLazy();   
            Container.BindInterfacesAndSelfTo<CharacterVitalsStorage>().AsSingle();
        }
    }
}
