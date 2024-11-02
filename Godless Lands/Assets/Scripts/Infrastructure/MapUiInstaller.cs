using Drop.UI;
using Network.Object.Interaction.UI;
using Trade.UI;
using UI.ConfirmationDialog;
using UI.PlayerCharacterDead;
using UnityEngine;
using Windows;
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
        private SelectQuantityWindow _selectQuantityWindow;
        [SerializeField]
        private ConfirmationDialogWindow _confirmationWindow;

        public override void InstallBindings()
        {
            Container.Bind<PlayerCharacterDeadWindow>().FromInstance(_playerCharacterDeadWindow).AsSingle();
            Container.Bind<InteractionIndicator>().FromInstance(_interactionIndicator).AsSingle();
            Container.Bind<DropWindow>().FromInstance(_dropWindow).AsSingle();
            Container.Bind<SelectQuantityWindow>().FromInstance(_selectQuantityWindow).AsSingle();
            Container.Bind<ConfirmationDialogWindow>().FromInstance(_confirmationWindow).AsSingle();

            foreach(var window in GetComponentsInChildren<Window>())
            {
                Container.BindInterfacesAndSelfTo(window.GetType()).FromInstance(window).AsSingle();
            }
        }
    }
}
