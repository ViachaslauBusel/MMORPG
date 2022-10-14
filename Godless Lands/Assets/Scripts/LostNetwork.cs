using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LostNetwork : MonoBehaviour {

    private Canvas canvas;
    private NetworkManager networkManager;

    [Inject]
    private void Construct(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        canvas.enabled = !networkManager.isConnected;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
