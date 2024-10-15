using Cells;
using Items;
using Protocol.Data.Items;
using Recipes;
using System.Collections.Generic;
using UnityEngine;
using CraftingStations;
using Zenject;

public class SmelterCell : ItemCell
{
    private SmelterModel _smelterModel;

    [Inject]
    private void Construct(SmelterModel smelterModel)
    {
        _smelterModel = smelterModel;
    }

    public override void Use()
    {
        HandleItemPut(this, null);
    }

    public override void Put(Cell cell)
    {
        if (cell is ItemCell itemCell && itemCell.IsEmpty() == false)
        {
            HandleItemPut(this, itemCell.GetItem());
            HandleItemPut(itemCell, null);
        }
    }

    private void HandleItemPut(ItemCell itemCell, Item item)
    {
        if (itemCell is SmelterCell smelterCell)
        {
           switch (smelterCell.StorageType)
            {
                case ItemStorageType.SmelerComponent:
                    _smelterModel.AddComponent(smelterCell.GetIndex(), item);
                    break;
                case ItemStorageType.SmelterFuel:
                    _smelterModel.AddFuel(smelterCell.GetIndex(), item);
                    break;
            }
        }
    }

    public override void PutItem(Item item)
    {

        _item = item;
        DrawCount();
        DrawIcon();
    }

    public void SetCount(IReadOnlyCollection<ItemBundleLink> pieces)
    {
        if (!IsEmpty() && _item.Data.IsStackable)
        {
            foreach (var piece in pieces)
            {
                if (piece.ID == _item.Data.ID)
                {
                    if (piece.Amount <= _item.Count) _countTxt.color = Color.green;
                    else _countTxt.color = Color.red;
                    _countTxt.text = _item.Count.ToString() + '/' + piece.Amount.ToString();
                    return;
                }
            }
        }
        DrawCount();
    }

    private void DrawIcon()
    {
        if (IsEmpty())
        {
            Hide();
        }
        else
        {
            Show();
            UpdateIcon();
        }
    }

    public void DrawCount()
    {
        _countTxt.color = Color.white;
        if (!IsEmpty() && _item.Data.IsStackable)
        {
            _countTxt.text = _item.Count.ToString()+"/0";
        }
        else
            _countTxt.text = "";
    }

    public override void Abort()
    {
        HandleItemPut(this, null);
    }
}
