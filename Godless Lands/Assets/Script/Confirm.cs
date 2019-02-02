using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirm : MonoBehaviour
{
    public bool butYes = false;
    public bool butNo = false;
    public Text text;
    private Canvas canvas;

    public static Confirm instant;

    private void Start()
    {
        if (instant != null) Destroy(gameObject);
        instant = this;

        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void SetTitle(string title)
    {
        text.text = title;
    }

    /// <summary>
    /// Возвращает 1 - при нажатии на кнопку да, 0 - нет, -1 кнопка еще не нажата
    /// </summary>
    /// <returns></returns>
    public int IsConfirm()
    {
        canvas.enabled = true;
        if (butYes) { Reset(); return 1; }
        if (butNo) { Reset(); return 0; }
        return -1;
    }

    private void Reset()
    {
        canvas.enabled = false;
        butNo = false;
        butYes = false;
    }

    public void ButtonYes()
    {
        butYes = true;
    }

    public void ButtonNo()
    {
        butNo = true;
    }

}
