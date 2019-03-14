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
    private int size_bag;
    [HideInInspector]
    public ItemCell[] items;
    private ItemsList itemsList;


    public void UpdateInventory(NetworkWriter nw)//Обновление содержимого ячейки
    {
        int filling = nw.ReadInt();//Заполнение рюкзака
                                   //  size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;



        weight.UpdateWeight(nw.ReadInt());


        int index = nw.ReadInt();//Индекс изменившейся ячейки
        int key = nw.ReadInt();//Уnикальный ид предмета

        if (index >= 0 && index < items.Length)
        {
            items[index].SetKey(key);
            if (key == 0) { items[index].PutItem(null, 0); return; }
        }
        else return;


        int id_item = nw.ReadInt();//Индекс нового предмета в этой ячейке
        int count = nw.ReadInt();
        items[index].PutItem(itemsList.CreateItem(id_item), count);

        if (nw.AvailableBytes > 0)
        {
            items[index].SetEnchantLevel(nw.ReadInt());
            items[index].SetDurabilty(nw.ReadInt());
            items[index].SetMaxDurabilty(nw.ReadInt());
        }

    }


    public void LoadingInventory(NetworkWriter nw, ItemsList itemsList)
    {
        this.itemsList = itemsList;
        int filling = nw.ReadInt();
        size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;

        weight.LoadWeight(nw.ReadInt(), nw.ReadInt());
      

        items = new ItemCell[size_bag];

        //  int count_item;

        Item item;
        for (int i = 0; i < items.Length; i++)//Создать пустые ячейки
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

                items[index].SetKey(key);
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
        }


     //   if (refreshCount != null) refreshCount();
    }
}
