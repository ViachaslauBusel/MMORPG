using Items.UI;
using Shop.Models;
using UnityEngine;
using UnityEngine.UI;
using Windows;
using Zenject;

namespace Shop.UI
{
    public class StoreWindow : Window
    {
        private StoreController _storeController;

        [Inject]
        private void Construct(StoreController storeController)
        {
            _storeController = storeController;
        }

        public override void Close()
        {
            base.Close();
            _storeController.CloseStore();
        }
    }
}
