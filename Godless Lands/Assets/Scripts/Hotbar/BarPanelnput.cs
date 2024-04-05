using Cells;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Hotbar
{
    public class BarPanelnput : MonoBehaviour
    {
        public string panel = "Main";
        private BarCell[] _barCells;
        private InputManager _inputManager;

        [Inject]
        private void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Awake()
        {
            _barCells = GetComponentsInChildren<BarCell>();
            _inputManager.Enable();
        }

        private void Start()
        {
            for (int i = 0; i < _barCells.Length; i++)
            {

                string actionName = $"Hotbar_F{i + 1}";
                InputAction input = _inputManager.FindAction(actionName);
                if (input == null) continue;
                _barCells[i].SetToken(input, (i+1).ToString());
            }
        }
    }
}