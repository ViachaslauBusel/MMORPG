using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using Items;

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
        RegisteredTypes.RegisterTypes(Types.FindDrop, FindDrop);
    }

    private void FindDrop(NetworkWriter nw)
    {
        ClearDropList();
        monster_layer = nw.ReadInt();
        monster_id = nw.ReadInt();
        print("monster id: " + monster_id + " monster layer: " + monster_layer);
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
        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.TakeDrop);
        nw.write(monster_layer);
        nw.write(monster_id);
        nw.write(id_item);

        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.FindDrop);
    }
}
