using Cells;
using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public Transform content;
    public Text text_filling;
    public BarWeight weight;
    public GameObject itemCell;
    private int maxCell;
    [HideInInspector]
    public List<ItemCell> items;


    public void UpdateInventory(NetworkWriter nw)//Обновление содержимого ячейки
    {
        int filling = nw.ReadInt();//Заполнение рюкзака
        maxCell = nw.ReadInt();//Всего ячеек

        if (items == null || items.Count != maxCell)
        {
            UpdateCellsCount();
        }

        text_filling.text = filling + "/" + this.maxCell;


        //текущий и максимальный вес
        weight.UpdateWeight(nw.ReadInt(), nw.ReadInt());

        while (nw.AvailableBytes > 0)
        {

            int index = nw.ReadInt();//Индекс изменившейся ячейки
            int itemID = nw.ReadInt();//ид предмета

            if (index >= 0 && index < items.Count)
            {
                //  items[index].SetObjectID(objetcID);
                if (itemID == 0) { items[index].PutItem(null, 0); return; }
            }
            else return;


            int objectID = nw.ReadInt();//Индекс нового предмета в этой ячейке
            int itemCount = nw.ReadInt();

            items[index].SetObjectID(objectID);
            items[index].PutItem(Inventory.Instance.itemsList.CreateItem(itemID), itemID);

            if (nw.ReadBool())//Если предмет элемент экеперовки
            {
                items[index].SetEnchantLevel(nw.ReadInt());
                items[index].SetDurabilty(nw.ReadInt());
                items[index].SetMaxDurabilty(nw.ReadInt());
            }
        }
    }

    private void UpdateCellsCount()
    {
        if (items == null)
            items = new List<ItemCell>();

        //Создать пустые ячейки
        for (int i=items.Count; i<maxCell; i++)
        {
            GameObject _obj = Instantiate(itemCell);
            _obj.transform.SetParent(content);
            items.Add(_obj.GetComponent<ItemCell>());
            items[i].SetIndex(i);
        }
        //Удалить ячейки
        for(int i=items.Count-1; i>=maxCell; i--)
        {
            Destroy(items[i].gameObject);
            items.RemoveAt(i);
        }
    }


  /*  public void LoadingInventory(NetworkWriter nw, ItemsList itemsList)
    {
        this.itemsList = itemsList;
        int filling = nw.ReadInt();
        maxCell = nw.ReadInt();
        text_filling.text = filling + "/" + maxCell;

        weight.LoadWeight(nw.ReadInt(), nw.ReadInt());
      

        items = new ItemCell[maxCell];

        //  int count_item;

        Item item;
        for (int i = 0; i < items.Length; i++)
        {
            GameObject _obj = Instantiate(itemCell);
            _obj.transform.SetParent(content);
            items[i] = _obj.GetComponent<ItemCell>();
            items[i].SetIndex(i);
        }

        while (nw.AvailableBytes > 0)
        {
            int index = nw.ReadInt();
            int key = nw.ReadInt();
            int id_item = nw.ReadInt();
            int count = nw.ReadInt();

            if (index >= 0 && index < items.Length)
            {

                items[index].SetObjectID(key);
                items[index].PutItem(itemsList.CreateItem(id_item), count);
            }

            if (nw.ReadBool())
            {
                int enchant_level = nw.ReadInt();
                int durability = nw.ReadInt();
                int maxDurability = nw.ReadInt();
                if (index > 0 && index < items.Length)
                {
                    items[index].SetEnchantLevel(enchant_level);
                    items[index].SetDurabilty(durability);
                    items[index].SetMaxDurabilty(maxDurability);
                }
            }
        }*/


     //   if (refreshCount != null) refreshCount();
   // }
}
