using Cells;
using Items;
using Recipes;
using RUCP;
using System.Collections.Generic;
using UnityEngine;

public class ActionCell : ItemCell
{
   // private Machine machine;
  //  private Components components;
    private ItemCell itemCell;
    public bool fuel;
   // protected int count;
   // protected Text countTxt;
  //  protected Item item;

    private new void Awake()
    {
        base.Awake();
    //    machine = GetComponentInParent<Machine>();
    //    components = GetComponentInParent<Components>();
    }
  /*  public override bool IsEmpty()
    {
        if (item == null) return true;
        if (item.id <= 0) return true;
        return false;
    }*/
    
    public override void Use()
    {
      
    }
    public override void Put(Cell cell)
    {
        if (cell == null) return;
        itemCell = cell as ItemCell;
        if (itemCell == null || itemCell.IsEmpty()) return;
        //  if (components.ConstainsItem(itemCell.GetItem().id)) return;//Если этот предмет уже есть в списке
        //   PutItem(itemCell.GetItem(), itemCell.GetCount(), itemCell.GetKey());//Установить иконку

        //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.MachineAddComponent);
        //nw.WriteBool(fuel);
        //nw.WriteInt(index);
        //nw.WriteInt(itemCell.GetObjectID());
        //NetworkManager.Send(nw);

    }

    public override void PutItem(Item item)
    {
        
        this._item = item;
        ClearCount();
        if (IsEmpty())
        {
            HideIcon();
            return;
        }
      //  item.count = count;
        ShowIcon();
        icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void SetCount(List<Piece> pieces)
    {
        if (!IsEmpty() && _item.stack)
        {
            foreach (Piece piece in pieces)
            {
                if (piece.ID == _item.id)
                {
                    if (piece.count <= _item.count) countTxt.color = Color.green;
                    else countTxt.color = Color.red;
                    countTxt.text = _item.count.ToString() + '/' + piece.count.ToString();
                    return;
                }
            }
        }
        ClearCount();
    }

    public void ClearCount()
    {
        countTxt.color = Color.white;
        if (!IsEmpty() && _item.stack)
        {
            countTxt.text = _item.count.ToString()+"/0";
        }
        else
            countTxt.text = "";
    }

   /* public int ID()
    {
        if (item == null) return 0;
        return item.id;
    }*/
    public override void Abort()
    {
    //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.MachineRemoveComponent);
        //nw.WriteBool(fuel);
        //nw.WriteInt(index);
        //NetworkManager.Send(nw);
    }
}
