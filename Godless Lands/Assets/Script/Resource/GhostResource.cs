using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostResource : MonoBehaviour, Action
{
    public int ID;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
}