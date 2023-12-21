using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Tools
{
    public static Vector3 ClearY(this Vector3 vector)
    {
        vector.y = 0.0f;
        return vector;
    }
    public static Vector3 GetClearY(this Vector3 vector)
    {
        return new Vector3(vector.x, 0.0f, vector.z);
    }
}
