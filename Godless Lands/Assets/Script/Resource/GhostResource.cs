using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostResource : MonoBehaviour, Action
{
    public int ID;
    public GameObject lod;
    public GameObject fragments;
    private Animator animator;
    private BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
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
        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.ResourceUse);
        nw.write(ID);
        NetworkManager.Send(nw);
    }

    public void DestroyAnim()
    {
        collider.enabled = false;
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