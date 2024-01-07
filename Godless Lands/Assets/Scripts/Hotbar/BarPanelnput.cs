using Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InpuPanelBar;

namespace Hotbar
{
    public class BarPanelnput : MonoBehaviour
    {
        public string panel = "Main";
        private BarCell[] barCells;

        private void Awake()
        {
            barCells = GetComponentsInChildren<BarCell>();
        }

        private void Start()
        {
            for(int i=0; i<barCells.Length; i++)
            { 

                Token token = InputManager.GetToken(PlayerPrefs.GetInt("MainCell" + i, i));

                barCells[i].SetToken(token);
            }
        }

        //Если какая то ячейка в панели использует эту клавишу true
        public bool IsUse(Token token)
        {
            foreach(BarCell cell in barCells)
            {
                if (cell.IsUse(token)) return true;
            }
            return false;
        }
    }
}