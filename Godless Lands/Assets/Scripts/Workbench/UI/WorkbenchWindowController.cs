using Machines;
using Protocol.Data.Workbenches;
using UnityEngine;
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

        private void OnWindowCommand(WorkbenchType type, bool openWin, bool isReadyForWork)
        {
            Debug.Log($"OnWindowCommand: {type}, {openWin}, {isReadyForWork}");
           if(openWin)
            {
                if(_openedWindow != null)
                {
                    if (_openedWindow.WorkbenchType != type)
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
                if(_openedWindow != null && _openedWindow.WorkbenchType == type)
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

        private IWorkbenchWindow GetWindow(WorkbenchType type) => type switch
        {
            WorkbenchType.Smelter => _smelterWindow,
            WorkbenchType.Grindstone => _grindstoneWindow,
            WorkbenchType.Tannery => _tanneryWindow,
            WorkbenchType.Workbench => _recipeCraftingWindow,
            _ => null
        };

        private void OnDestroy()
        {
            _workbenchListener.OnWindowCommand -= OnWindowCommand;
        }
    }
}
