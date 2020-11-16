using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_CanvasRotate : MonoBehaviour {

    private Transform _camera;

    // Use this for initialization
    void Start () {
        _camera = GameObject.Find("Main Camera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.position);
	}
}
