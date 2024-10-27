using Items.UI;
using Protocol.Data.Items;
using Shop.Models;
using System.Linq;
using UnityEngine;
using Windows;
using Zenject;

namespace Shop.UI
{
    public class BuyTabRenderer : MonoBehaviour
    {
        [SerializeField]
        private ItemStorageRenderer _itemsForSale, _itemsPurchaseByPlayerRender;
        private StoreController _storeController;
        private StoreModel _storeModel;
        private StoreWindow _parentWindow;

        [Inject]
        private void Construct(StoreModel storeModel, StoreController storeController)
        {
            _storeModel = storeModel;
            _storeController = storeController;
        }

        private void Awake()
        {
            _parentWindow = GetComponentInParent<StoreWindow>();
        }

        private void Start()
        {
            _itemsForSale.Initialize(_storeModel.ItemsForSale);
            _itemsPurchaseByPlayerRender.Initialize(_storeModel.ItemsPurchasedByPlayer);
        }

        public void BuyItems()
        {
            _parentWindow.Hide();
            _storeController.BuyItems(_storeModel.ItemsPurchasedByPlayer.Items.Where(item => !item.IsEmpty).Select(i => new ItemBundleNData(i.Data.ID, i.Count)).ToList());
        }
    }
}
