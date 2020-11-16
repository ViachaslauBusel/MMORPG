using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    private float timer;

    private void Awake()
    {
        enabled = false;
    }

    public void StartTimer(float timer)
    {
        this.timer = timer;
        enabled = true;
    }

    private void Update()
    {
        if(timer <= 0.0f)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}
