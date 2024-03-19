using Cells;
using Inventory;
using Inventory.UI;
using Items;
using UnityEngine;
using Zenject;

public delegate void Update();
public class InventoryWindow : MonoBehaviour
{

    [SerializeField]
    private DrawBagContents _primaryBag;
    [SerializeField]
    private DrawBagContents _secondaryBag;
    private Canvas _canvasInventory;
    private InventoryModel _inventory;
    private UISort _uISort;


    [Inject]
    private void Construct(InventoryModel inventoryModel)
    {
        _inventory = inventoryModel;
    }

    private void Awake()
    {
        _uISort = GetComponentInParent<UISort>();
        _canvasInventory = GetComponent<Canvas>();

        _canvasInventory.enabled = false;

        _primaryBag.Init(_inventory.PrimaryBag);
        _secondaryBag.Init(_inventory.SecondaryBag);
    }

    public Item GetItemByObjectID(long objectID) => GetItemCellByObjectID(objectID)?.GetItem();
    
    public ItemCell GetItemCellByObjectID(long objectID)
    {
        if (_primaryBag.TryGetItemCellByItemUID(objectID, out ItemCell itemCell)
        || _secondaryBag.TryGetItemCellByItemUID(objectID, out itemCell))
        {
            return itemCell;
        }

        return null;// _armor.GetItemCell(objectID);
    }

    public static void RegisterUpdate(Update refresh)
    {
       // Instance.update += refresh;
    }

    public static void UnregisterUpdate(Update refresh)
    {
       // Instance.update -= refresh;
    }

    public void OpenCloseInventory()
    {
        _canvasInventory.enabled = !_canvasInventory.enabled;

        if(_canvasInventory.enabled) _uISort.PickUp(_canvasInventory);
    }
}

