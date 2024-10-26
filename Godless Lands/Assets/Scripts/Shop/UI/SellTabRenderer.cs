using Items.UI;
using Protocol.Data.Items;
using Shop.Models;
using System.Linq;
using UnityEngine;
using Windows;
using Zenject;

namespace Shop.UI
{
    public class SellTabRenderer : MonoBehaviour
    {
        [SerializeField]
        private ItemStorageRenderer _itemsSoldByPlayerRender;
        private StoreModel _shopModel;
        private StoreController _storeController;
        private Window _parentWindow;

        [Inject]
        private void Construct(StoreModel shopModel, StoreController storeController)
        {
            _shopModel = shopModel;
            _storeController = storeController;
        }

        private void Awake()
        {
            _parentWindow = GetComponentInParent<Window>();
        }

        private void Start()
        {
            _itemsSoldByPlayerRender.Initialize(_shopModel.ItemsSoldByPlayer);
        }

        public void SellItems()
        {
            _storeController.SellItems(_shopModel.ItemsSoldByPlayer.Items.Where(item => !item.IsEmpty).Select(i => new ItemBundleNData(i.Data.ID, i.Count)).ToList());
            _parentWindow.Close();
        }
    }
}
