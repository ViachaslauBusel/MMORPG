using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour, ITargetObject
{
    public int id;
    private float endTime;

    public void SetEndTime(int time)
    {
        endTime = Time.time + (time / 1000.0f);
    }

    public int GetMin()
    {
        return (int)Math.Truncate((endTime - Time.time) / 60.0f);
    }

    public int GetSec()
    {
        return (int)Math.Truncate((endTime - Time.time) % 60.0f);
    }

    public string GetName()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int Id()
    {
        return id;
    }

    public bool IsAlive()
    {
        throw new System.NotImplementedException();
    }

    public int Layer()
    {
        return 4;
    }

    public void OffTarget()
    {
        throw new System.NotImplementedException();
    }

    public void OnTarget()
    {
        throw new System.NotImplementedException();
    }
}
