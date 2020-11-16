using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISort : MonoBehaviour {

    private Canvas[] canvasSorted;

    private void Start()
    {
        WindowMove[] windows = transform.GetComponentsInChildren<WindowMove>();
        canvasSorted = new Canvas[windows.Length];
        for(int i=0; i<windows.Length; i++)
        {
            canvasSorted[i] = windows[i].GetComponentInParent<Canvas>();
            canvasSorted[i].sortingOrder = 1;
        }
    }

    public void PickUp(Canvas canvas)
    {
        for (int i = 0; i < canvasSorted.Length; i++)
        {
            canvasSorted[i].sortingOrder = 1;
        }
        canvas.sortingOrder = 2;
    }
}
