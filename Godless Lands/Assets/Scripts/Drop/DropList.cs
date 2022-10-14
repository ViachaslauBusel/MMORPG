using Items;
using RUCP;
using RUCP.Handler;
using System.Collections;
using UnityEngine;
using Zenject;

public class DropList : MonoBehaviour {

    public ItemsContainer itemsList;
    public GameObject cellItemDrop;
    public Transform contentParent;

    private Canvas canvas;
    private ArrayList dropList;

    private int monster_layer;
    private int monster_id;
    private NetworkManager networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.FindDrop, FindDrop);
    }


    private void FindDrop(Packet nw)
    {
        ClearDropList();
        monster_layer = nw.ReadInt();
        monster_id = nw.ReadInt();
    //    print("monster id: " + monster_id + " monster layer: " + monster_layer);
        while (nw.AvailableBytesForReading > 0)
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
    //TODO msg
        //Packet nw = new Packet(Channel.Reliable);
        //nw.WriteType(Types.TakeDrop);
        //nw.WriteInt(monster_layer);
        //nw.WriteInt(monster_id);
        //nw.WriteInt(id_item);

        //NetworkManager.Send(nw);

        if (id_item == -1) Close();//-1 take all drop and close window
    }

    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.FindDrop);
    }
}
