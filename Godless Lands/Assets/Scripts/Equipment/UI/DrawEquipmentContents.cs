using Cells;
using Equipment;
using Items;
using Protocol.MSG.Game.Equipment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrawEquipmentContents : MonoBehaviour
{
    private EquipmentModel _equipment;
    private Dictionary<EquipmentType, ArmorCell> _armorCells = new Dictionary<EquipmentType, ArmorCell>();

    [Inject]
    private void Construct(EquipmentModel equipment)
    {
        _equipment = equipment;
    }

    private void Awake()
    {
        var cells = GetComponentsInChildren<ArmorCell>();

        foreach (var cell in cells)
        {
            EquipmentType eqType = cell.CellType;

            if (eqType == EquipmentType.None)
            {
                Debug.LogError($"[{cell.CellType}] Equipment type not found");
                continue;
            }

            if(_armorCells.ContainsKey(eqType))
            {
               Debug.LogError($"[{cell.CellType}][{eqType}]Armor cell already registered");
                continue;
            }
            
            _armorCells.Add(eqType, cell);
        }

        _equipment.OnItemsChanged += OnItemsChanged;
    }

    private void OnItemsChanged()
    {
        foreach (var item in _equipment.Items)
        {
            if (item == null)
            {
                Debug.LogError("Item is null");
            }

            EquipmentType eqType = (EquipmentType)item.SlotIndex;
            if(_armorCells.ContainsKey(eqType) == false)
            {
                //Debug.LogError($"[{eqType}] Armor cell not found");
                continue;
            }
            _armorCells[eqType].PutItem(item);
        }
    }
}
