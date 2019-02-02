#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform spawnPoint;

    public Vector3 GetPoint()
    {
        return spawnPoint.position;
    }
}
#endif