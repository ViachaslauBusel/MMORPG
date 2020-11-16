using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInformer : MonoBehaviour
{
    private static DisplayInformer informer;
    private static bool active = false;
    private GameObject activeObj;
    private Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        informer = this;
        gameObject.SetActive(false);
    }

    public static void On(GameObject obj)
    {

        active = true;
        informer.activeObj = obj;
        if (informer.gameObject.activeSelf != active) informer.gameObject.SetActive(active);
    }

    public static void Off()
    {
        active = false;
        if (informer.gameObject.activeSelf != active) informer.gameObject.SetActive(active);
    }

    private void Update()
    {

      
        if (!active) return;
        Corpse corpse = activeObj.GetComponent<Corpse>();
        text.text = "Время: " + corpse.GetMin() + "мин " + corpse.GetSec() + "сек";
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y-50.0f, Input.mousePosition.z);
    }
}
