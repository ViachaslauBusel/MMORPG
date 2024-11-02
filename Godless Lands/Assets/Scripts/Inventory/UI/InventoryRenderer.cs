using Items.UI;
using Protocol.Data.Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Inventory.UI
{
    public class InventoryRenderer : MonoBehaviour
    {
        [SerializeField]
        private Button _primaryBagBut, _secondaryBagBut;
        [SerializeField]
        private ItemStorageRenderer _storageRender;
        [SerializeField]
        private ItemStorageSlotUsageDisplay _storageSlotUsageRender;
        [SerializeField]
        private ItemStorageWeightDisplay _storageWeightRender;
        private InventoryModel _inventory;
        private ItemStorageType _currentBagType;

        [Inject]
        private void Construct(InventoryModel inventoryModel)
        {
            _inventory = inventoryModel;
        }

        private void Awake()
        {
            _primaryBagBut.onClick.AddListener(SwitchBagToPrimary);
            _secondaryBagBut.onClick.AddListener(SwitchBagToSecondary);
        }

        private void Start()
        {
            SwitchBagToPrimary();
        }

        private void SwitchBagToSecondary()
        {
            _currentBagType = ItemStorageType.SecondaryBag;
            SwitchBag();
        }

        private void SwitchBagToPrimary()
        {
            _currentBagType = ItemStorageType.PrimaryBag;
            SwitchBag();
        }

        private void SwitchBag()
        {
            _primaryBagBut.interactable = _currentBagType != ItemStorageType.PrimaryBag;
            _secondaryBagBut.interactable = _currentBagType != ItemStorageType.SecondaryBag;
            var storage = _currentBagType == ItemStorageType.PrimaryBag ? _inventory.PrimaryBag : _inventory.SecondaryBag;

            _storageRender.Initialize(storage);
            _storageSlotUsageRender?.Initialize(storage);
            _storageWeightRender?.Initialize(storage);
        }
    }
}
