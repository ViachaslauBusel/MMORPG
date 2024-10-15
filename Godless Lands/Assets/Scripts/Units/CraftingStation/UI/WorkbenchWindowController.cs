using Protocol.Data.Units.CraftingStation;
using UnityEngine;
using CraftingStations.UI.RecipeCrafting;
using CraftingStations.UI.ComponentFuelCrafting;
using Zenject;

namespace CraftingStations.UI
{
    public class WorkbenchWindowController : MonoBehaviour
    {
        [SerializeField]
        private ComponentFuelCraftingWindow _smelterWindow;
        [SerializeField]
        private ComponentFuelCraftingWindow _grindstoneWindow;
        [SerializeField]
        private ComponentFuelCraftingWindow _tanneryWindow;
        [SerializeField]
        private RecipeCraftingWindow _recipeCraftingWindow;
        private ICraftWindow _openedWindow;
        private CraftingStationListener _workbenchListener;

        [Inject]
        public void Construct(CraftingStationListener workbenchListener)
        {
            _workbenchListener = workbenchListener;
        }

        private void Awake()
        {
            _workbenchListener.OnWindowCommand += OnWindowCommand;
        }

        private void OnWindowCommand(CraftingStationType type, bool openWin, bool isReadyForWork)
        {
            Debug.Log($"OnWindowCommand: {type}, {openWin}, {isReadyForWork}");
           if(openWin)
            {
                if(_openedWindow != null)
                {
                    if (_openedWindow.StationType != type)
                    {
                        Debug.LogError("Trying to open already opened window");
                        _openedWindow.Close();
                    }
                    else _openedWindow.SetStatus(isReadyForWork);
                }
                _openedWindow = GetWindow(type);
                _openedWindow.Open(isReadyForWork);
            }
            else
            {
                if(_openedWindow != null && _openedWindow.StationType == type)
                {
                    _openedWindow.Close();
                    _openedWindow = null;
                }
                else
                {
                    Debug.LogError("Trying to close not opened window");
                }
            }
        }

        private ICraftWindow GetWindow(CraftingStationType type) => type switch
        {
            CraftingStationType.Smelter => _smelterWindow,
            CraftingStationType.Grindstone => _grindstoneWindow,
            CraftingStationType.Tannery => _tanneryWindow,
            CraftingStationType.Workbench => _recipeCraftingWindow,
            _ => null
        };

        private void OnDestroy()
        {
            _workbenchListener.OnWindowCommand -= OnWindowCommand;
        }
    }
}
