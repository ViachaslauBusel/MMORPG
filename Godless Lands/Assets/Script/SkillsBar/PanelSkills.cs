using Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillsBar
{
    public class PanelSkills : MonoBehaviour
    {
        private static BarCell[] barCells;

        private void Awake()
        {
            barCells = GetComponentsInChildren<BarCell>();
        }

        public static void Hide(float time)
        {
            foreach(BarCell cell in barCells)
            {
                cell.Hide(time);
            }
        }
    }
}