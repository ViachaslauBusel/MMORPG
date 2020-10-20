using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using Items;
using RUCP.Packets;
using RUCP.Network;

public class DropList : MonoBehaviour {

    public ItemsList itemsList;
    public GameObject cellItemDrop;
    public Transform contentParent;

    private Canvas canvas;
    private ArrayList dropList;

    private int monster_layer;
    private int monster_id;

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.FindDrop, FindDrop);
    }

    private void FindDrop(Packet nw)
    {
        ClearDropList();
        monster_layer = nw.ReadInt();
        monster_id = nw.ReadInt();
    //    print("monster id: " + monster_id + " monster layer: " + monster_layer);
        while (nw.AvailableBytes > 0)
        {
            int id_item = nw.ReadInt();
            GameObject _obj = Instantiate(cellItemDrop);
            _obj.transform.SetParent(contentParent);
            _obj.GetComponent<CellItemDrop>().SetItem(itemsList.GetItem(id_item));
            dropList.Add(_obj);
        }
        canvas.enabled = true;
    }

    private void ClearDropList()
    {
        foreach (GameObject _obj in dropList)
        {
            Destroy(_obj);
        }
        dropList.Clear();
    }
    public void Close()
    {
        ClearDropList();
        canvas.enabled = false;
    }

    private void Start()
    {
        dropList = new ArrayList();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void TakeDrop(int id_item)
    {
        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.TakeDrop);
        nw.WriteInt(monster_layer);
        nw.WriteInt(monster_id);
        nw.WriteInt(id_item);

        NetworkManager.Send(nw);

        if (id_item == -1) Close();//-1 take all drop and close window
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.FindDrop);
    }
}
