using Protocol.Data.Units.CraftingStation;
using UnityEngine;
using Workbench.UI.RecipeCrafting;
using Workbench.UI.Smelter;
using Zenject;

namespace Workbench.UI
{
    public class WorkbenchWindowController : MonoBehaviour
    {
        [SerializeField]
        private SmelterWindow _smelterWindow;
        [SerializeField]
        private SmelterWindow _grindstoneWindow;
        [SerializeField]
        private SmelterWindow _tanneryWindow;
        [SerializeField]
        private RecipeCraftingWindow _recipeCraftingWindow;
        private IWorkbenchWindow _openedWindow;
        private WorkbenchListener _workbenchListener;

        [Inject]
        public void Construct(WorkbenchListener workbenchListener)
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

        private IWorkbenchWindow GetWindow(CraftingStationType type) => type switch
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
