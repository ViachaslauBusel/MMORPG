using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetObject {

    Vector3 GetPosition();
    void OnTarget();
    void OffTarget();
    string GetName();
    Transform GetTransform();
    int Layer();
    int Id();
    bool IsAlive();
}
