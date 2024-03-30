using RUCP;
using RUCP.Handler;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MachinesManager : MonoBehaviour, Manager
{
    public GameObject smelterPref;
    public GameObject grindstonePref;
    public GameObject workbenchPref;
    public GameObject tanneryPref;
    private static Dictionary<int, GameObject> machines;
    private NetworkManager networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        networkManager.RegisterHandler(Types.MachineCreate, MachineCreate);
        networkManager.RegisterHandler(Types.MachineDelete, MachineDelete);
    }


    private void Start()
    {

        machines = new Dictionary<int, GameObject>();
    }

    public void AllDestroy()
    {
        foreach (GameObject obj in machines.Values)
            Destroy(obj);
        machines.Clear();

    }

    private void MachineDelete(Packet nw)
    {
        int id = nw.ReadInt();
        if (machines.ContainsKey(id))
        {
            Destroy(machines[id]);
            machines.Remove(id);
        }
    }

    private void MachineCreate(Packet nw)
    {
        int id = nw.ReadInt();
        MachineUse machineUse = (MachineUse) nw.ReadByte();
        Vector3 postion = Vector3.zero;//TODO nw.ReadVector3();
        Vector3 rotation = Vector3.zero; //TODO nw.ReadVector3();
        if (machines.ContainsKey(id)) { print("error create machine"); return; } //Если монстр с таким ид уже создан


        //  GameObject prefabResource = resourceList.GetPrefab(idSkin);
        GameObject prefab;
        switch (machineUse)
        {
            case MachineUse.Smelter:
                prefab = smelterPref;
                break;
            case MachineUse.Grindstone:
                prefab = grindstonePref;
                break;
            case MachineUse.Workbench:
                prefab = workbenchPref;
                break;
            case MachineUse.Tannery:
                prefab = tanneryPref;
                break;
            default:
                print("Error machine skin: ");
                return;
        }
     

        GameObject _obj = Instantiate(prefab, postion, Quaternion.Euler(rotation));
        _obj.transform.SetParent(transform);

        React action = _obj.GetComponent<React>();
        action.SetID(id);
        machines.Add(id, _obj);
    }


    private void OnDestroy()
    {
        networkManager?.UnregisterHandler(Types.MachineCreate);
        networkManager?.UnregisterHandler(Types.MachineDelete);
    }
}
