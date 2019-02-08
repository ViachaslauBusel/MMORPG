﻿using Cells;
using Items;
using RUCP;
using RUCP.Handler;
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
        public ItemsList ItemsList;
        private static BarCell[] barCells;

        private void Awake()
        {
            barCells = GetComponentsInChildren<BarCell>();
            for(int i=0; i<barCells.Length; i++)
            {
                barCells[i].SetIndex(i);
            }
            RegisteredTypes.RegisterTypes(Types.loadBar, loadBar);
            RegisteredTypes.RegisterTypes(Types.updateBarCell, updateBarCell);
           
        }

        private void loadBar(NetworkWriter nw)
        {
            
        }

        private void updateBarCell(NetworkWriter nw)
        {
            int cell = nw.ReadByte();
            int type = nw.ReadInt();
            int id = nw.ReadInt();
            int key = nw.ReadInt();
            switch (type)
            {
                case -1://Очистить ячейку
                    barCells[cell].Clear();
                    break;
                case 0://skill
                    Skill skill = skillsList.GetSkill(id);
                    if (skill == null) return;
                    barCells[cell].SetSkill(skill);
                    break;
                case 1://item
                    Item item = ItemsList.GetItem(id);
                    if (item == null) return;
                    barCells[cell].SetItem(item, key);
                    break;
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
        }
    }
}