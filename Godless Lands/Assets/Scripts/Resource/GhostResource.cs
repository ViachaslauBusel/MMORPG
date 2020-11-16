using RUCP;
using RUCP.Network;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostResource : MonoBehaviour, React
{
    public int ID;
    public GameObject lod;
    public GameObject fragments;
    private Animator animator;
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        fragments.SetActive(false);
        ActionListener.Add(this);
    }
    public Vector3 position
    {
        get
        {
            return transform.position;
        }
    }

    public float distance
    {
        get
        {
            return 1.5f;
        }
    }

    public void SetName(string _name)
    {

    }

    public void Use()
    {
        Packet nw = new Packet(Channel.Reliable);
        nw.WriteType(Types.ResourceUse);
        nw.WriteInt(ID);
        NetworkManager.Send(nw);
    }

    public void DestroyAnim()
    {
        _collider.enabled = false;
        lod.SetActive(false);
        fragments.SetActive(true);
        animator.SetTrigger("destroy");
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ActionListener.Remove(this);
    }

    public MachineUse GetMachine()
    {
        throw new System.NotImplementedException();
    }
    public void SetID(int id)
    {
        ID = id;
    }
}