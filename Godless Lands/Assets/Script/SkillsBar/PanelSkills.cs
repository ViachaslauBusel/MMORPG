using Cells;
using Items;
using RUCP;
using RUCP.Handler;
using Skills;
using SkillsRedactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillsBar
{
    public class PanelSkills : MonoBehaviour
    {
        public SkillsList skillsList;
        private static BarCell[] barCells;

        private void Awake()
        {
            barCells = GetComponentsInChildren<BarCell>();
            for(int i=0; i<barCells.Length; i++)
            {
                barCells[i].SetIndex(i);
            }
         
    
            RegisteredTypes.RegisterTypes(Types.updateBarCell, updateBarCell);
           
        }
        private void Start()
        {
            Inventory.RegisterUpdate(UpdateInventory);
        }

        private void UpdateInventory()
        {
            foreach(BarCell cell in barCells)
            {
                cell.Refresh();
            }
        }


        private void updateBarCell(NetworkWriter nw)
        {
            while(nw.AvailableBytes > 0)
            {
                int index = nw.ReadByte();
                SkillbarType type = (SkillbarType)nw.ReadInt();
                int uniqueID = nw.ReadInt();
                if(index >= 0 && index < barCells.Length)
                {
                    Cell cell = null;
                    if (type == SkillbarType.Item)
                        cell = Inventory.GetItemCellByObjectID(uniqueID);
                    else if (type == SkillbarType.Skill)
                        cell = SkillsBook.GetSkillCellByID(uniqueID);

                    barCells[index].InsertCell(cell);
                }
            }
        }

        public static void Hide(float time)
        {
            foreach(BarCell cell in barCells)
            {
                cell.Hide(time);
            }
        }

        private void OnDestroy()
        {
            RegisteredTypes.UnregisterTypes(Types.updateBarCell);
            Inventory.UnregisterUpdate(UpdateInventory);
        }
    }
}