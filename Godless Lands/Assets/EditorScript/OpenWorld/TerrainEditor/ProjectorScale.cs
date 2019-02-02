#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorScale : MonoBehaviour {

    private Projector projector;

    private void Start()
    {
        projector = GetComponent<Projector>();
    }

    public void Scale(float scale)
    {
        
        projector.aspectRatio = scale;
    }
}
#endif