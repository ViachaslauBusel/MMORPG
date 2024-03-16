using Drop;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellItemDrop : MonoBehaviour {

    public Image item_icon;
    public Text item_name;
    public Button item_take;
    private int item_id;


    private void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void SetItem(Item _item)
    {
        item_icon.sprite = Sprite.Create(_item.texture, new Rect(0, 0, _item.texture.width, _item.texture.height), new Vector2(0.5f, 0.5f));
        item_name.text = _item.nameItem;

        item_id = _item.id;

        item_take.onClick.AddListener(() => Take());

        
    }

    public void Take()
    {
        //DropWindow dropList = GameObject.Find("CanvasDrop").GetComponentInChildren<DropWindow>();
        //dropList.TakeDrop(item_id);
        //Destroy(gameObject);
    }
}
