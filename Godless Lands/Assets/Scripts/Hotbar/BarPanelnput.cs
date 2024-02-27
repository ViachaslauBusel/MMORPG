using Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InpuPanelBar;
using Zenject;
using UnityEngine.InputSystem;

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
                _barCells[i].SetToken(input);
            }
        }

        private void Hotbar_F1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            throw new System.NotImplementedException();
        }

        //Если какая то ячейка в панели использует эту клавишу true
        public bool IsUse(Token token)
        {
            foreach(BarCell cell in _barCells)
            {
                if (cell.IsUse(token)) return true;
            }
            return false;
        }
    }
}