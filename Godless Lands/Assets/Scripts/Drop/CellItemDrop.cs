using Drop;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellItemDrop : MonoBehaviour {

    [SerializeField]
    private Image _item_icon_img;
    [SerializeField]
    private Text _item_name_txt;
    [SerializeField]
    private Button _item_take_btn;
    [SerializeField]
    private Text _countTxt;
    private int _itemSlotIndex;
    private DropWindowInputHandler _dropWindowInputHandler;

    private void Start()
    {
        _dropWindowInputHandler = GetComponentInParent<DropWindowInputHandler>();
        transform.localScale = Vector3.one;
    }

    public void SetItem(Item _item)
    {
        _item_icon_img.sprite = Sprite.Create(_item.Data.texture, new Rect(0, 0, _item.Data.texture.width, _item.Data.texture.height), new Vector2(0.5f, 0.5f));
        _item_name_txt.text = _item.Data.nameItem;

        _countTxt.text = _item.Data.stack ? _item.Count.ToString() : "";

        _itemSlotIndex = _item.SlotIndex;

        _item_take_btn.onClick.AddListener(() => Take());
    }

    public void Take()
    {
        _dropWindowInputHandler.TakeItem(_itemSlotIndex);
    }
}
