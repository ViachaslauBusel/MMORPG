﻿using RUCP;
using RUCP.Handler;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    public bool activeted = false;
    public float timeSend = 2.0f;
    private float timer;
    private Queue<Vector3> hitPoints;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.TestRay, VoidTestRay);
        hitPoints = new Queue<Vector3>();
    }

    private void VoidTestRay(NetworkWriter nw)
    {
        if (nw.ReadBool())
        {
            hitPoints.Enqueue(nw.ReadVec3());
        }
        else
        {
            print("dont hit");
        }

        if (hitPoints.Count > 1100) hitPoints.Dequeue();
    }

    private void Start()
    {
        timer = timeSend;
    }

    private void Update()
    {
        if (false)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = timeSend;
                NetworkWriter nw = new NetworkWriter(Channels.Reliable);
                nw.SetTypePack(Types.TestRay);
                nw.write(transform.position + (Vector3.up * 1000));
              //  nw.write(transform.position + (Vector3.down * 300));

                NetworkManager.Send(nw);
            }
        }
        if (Input.GetButton("Jump") && isSend)
        {
            isSend = false;
            print("Send tile");
            StartCoroutine(send());
        }
    }
     bool isSend = true;
    IEnumerator send()
    {
        int startX = (int)(transform.position.x) - 5;
        int startZ = (int)(transform.position.z) - 5;
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                Vector3 vector = new Vector3(startX + x, transform.position.y + 10, startZ + y);
                NetworkWriter nw = new NetworkWriter(Channels.Reliable);
                nw.SetTypePack(Types.TestRay);
                nw.write(vector);
                //  nw.write(transform.position + (Vector3.down * 300));

                NetworkManager.Send(nw);
                yield return new WaitForSeconds(0.01f);
            }
        }
        isSend = true;
    }

    private void OnDrawGizmos()
    {
    //   print("draw f");
        foreach(Vector3 hit in hitPoints)
        {
        //    print("Draw: " + hit);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit, 0.2f);
        }
    }

    private void OnDestroy()
    {

        RegisteredTypes.UnregisterTypes(Types.TestRay);
    }
}
