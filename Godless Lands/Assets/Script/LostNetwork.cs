using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostNetwork : MonoBehaviour {

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        canvas.enabled = !NetworkManager.Socket.IsConnected();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
