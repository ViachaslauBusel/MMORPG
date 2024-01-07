using Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotbar
{
    public class LockInfo : Tools
    {
        public Transform parentCells;
        private BarCellEvent[] barCells;

        private void Awake()
        {
            barCells = parentCells.GetComponentsInChildren<BarCellEvent>();
        }
        private new void Start()
        {
            base.Start();
            activated = PlayerPrefs.GetInt("SkillsBarLockInfo", 0) == 1;
        }

        public override void Activation()
        {
            foreach(BarCellEvent cell in barCells)
            {
                cell.SetLock(false);
            }
            PlayerPrefs.SetInt("SkillsBarLockInfo", 1);
        }
        public override void Deactivation()
        {

            foreach (BarCellEvent cell in barCells)
            {
                cell.SetLock(true);
            }
            PlayerPrefs.SetInt("SkillsBarLockInfo", 0);
        }
    }
}