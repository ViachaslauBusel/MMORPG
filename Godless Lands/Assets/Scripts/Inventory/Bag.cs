using Cells;
using Items;
using RUCP;
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
    public List<ItemCell> cells;


    public void UpdateInventory(Packet nw)//Обновление содержимого ячейки
    {

        int filling = nw.ReadInt();//Заполнение рюкзака
        maxCell = nw.ReadInt();//Всего ячеек

        if (cells == null || cells.Count != maxCell)
        {
            UpdateCellsCount();
           
        }

        text_filling.text = filling + "/" + this.maxCell;


        //текущий и максимальный вес
        weight.UpdateWeight(nw.ReadInt(), nw.ReadInt());

        while (nw.AvailableBytesForReading > 0)
        {
            int index = nw.ReadInt();
            Item item = nw.ReadItem();

            if (index >= 0 && index < cells.Count)
            {
                cells[index].PutItem(item);  
            }
        }
    }

    private void UpdateCellsCount()
    {
        if (cells == null)
            cells = new List<ItemCell>();

        //Создать пустые ячейки
        for (int i=cells.Count; i<maxCell; i++)
        {
            GameObject _obj = Instantiate(itemCell);
            _obj.transform.SetParent(content);
            cells.Add(_obj.GetComponent<ItemCell>());
            cells[i].SetIndex(i);
        }
        //Удалить ячейки
        for(int i=cells.Count-1; i>=maxCell; i--)
        {
            Destroy(cells[i].gameObject);
            cells.RemoveAt(i);
        }
    }
}
