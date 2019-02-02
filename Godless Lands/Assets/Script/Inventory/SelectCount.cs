using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCount : MonoBehaviour
{
    public InputField count_input;
    private Canvas canvas;
    private bool butYes = false;
    private bool butNo = false;
    public static SelectCount Select;

    private void Awake()
    {
        if (Select != null) Destroy(Select.gameObject);
        Select = this;
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public static int Count { get { return Select.GetCount(); } }

    /// <summary>
    /// Возвращает количество выбранных предметов, если выбор еще не сделан -1
    /// </summary>
    /// <returns></returns>
    public int GetCount()
    {
        canvas.enabled = true;
        if (butYes)
        {
            int count = 0;
            if (count_input.text.Length < 1) { count = 1; }
            else {Int32.TryParse(count_input.text, out count); }

            if (count < 0) count = 0;
            OffSelecter();
            return count;
        }
        if (butNo)
        {
            OffSelecter();
            return 0;
        }

        return -1;
    }

    private void OffSelecter()
    {
        canvas.enabled = false;
        butNo = false;
        butYes = false;
        count_input.text = "";
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
