using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ping : MonoBehaviour {

    private string ping_txt;


	// Update is called once per frame
	void Update () {
        ping_txt = NetworkManager.Socket.GetPing().ToString();
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), ping_txt + " ms");
    }
}
