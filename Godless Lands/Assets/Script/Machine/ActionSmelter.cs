using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSmelter : MonoBehaviour, Action
{
    private Canvas smelter;

    private void Awake()
    {
        smelter = GameObject.Find("Smelter").GetComponent<Canvas>();
        ActionListener.Add(this);
    }

    public Vector3 position
    {
        get { return transform.position; }
    }
    public float distance
    {
        get
        {
            return 2.2f;
        }
    }

    public void Use()
    {
        smelter.enabled = true;
    }

    private void OnDestroy()
    {
        ActionListener.Remove(this);
    }
}
