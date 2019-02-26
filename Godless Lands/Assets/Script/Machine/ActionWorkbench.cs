using Machines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWorkbench : MonoBehaviour, Action
{
    private Workbench workbench;
    private int ID;

    private void Start()
    {
        workbench = GameObject.Find("WorkbenchGUI").GetComponent<Workbench>();
        ActionListener.Add(this);
    }

    public Vector3 position { get { return transform.position; } }

    public float distance
    {
        get
        {
            return 2.2f;
        }
    }

    public void Use()
    {
        workbench.Open(ID);
    }

    public MachineUse GetMachine()
    {
        return MachineUse.Workbench;
    }

    public void SetID(int id)
    {
        ID = id;
    }

    private void OnDestroy()
    {
        ActionListener.Remove(this);
    }
}
