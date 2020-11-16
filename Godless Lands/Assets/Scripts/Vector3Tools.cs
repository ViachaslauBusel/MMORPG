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
}
