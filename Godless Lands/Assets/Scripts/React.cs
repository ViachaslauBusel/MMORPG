using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface React
{
    void Use();
    Vector3 position { get; }
    float distance { get; }
    //TODO
    MachineUse GetMachine();
    void SetID(int id);
}
