using Drop;
using ObjectInteraction.UI;
using UI.PlayerCharacterDead;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class MapUiInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerCharacterDeadWindow _playerCharacterDeadWindow;
        [SerializeField]
        private InteractionIndicator _interactionIndicator;
        [SerializeField]
        private DropWindow _dropWindow;
        [SerializeField]
        private InventoryWindow _inventoryWindow;
        [SerializeField]
        private DialogWindow _dialogWindow;

        public override void InstallBindings()
        {
            Container.Bind<PlayerCharacterDeadWindow>().FromInstance(_playerCharacterDeadWindow).AsSingle();
            Container.Bind<InteractionIndicator>().FromInstance(_interactionIndicator).AsSingle();
            Container.Bind<DropWindow>().FromInstance(_dropWindow).AsSingle();
            Container.Bind<InventoryWindow>().FromInstance(_inventoryWindow).AsSingle();
            Container.Bind<DialogWindow>().FromInstance(_dialogWindow).AsSingle();
        }
    }
}
