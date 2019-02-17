using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Action
{
    void Use();
    Vector3 position { get; }
    float distance { get; }
}
