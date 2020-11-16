using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectQuantity : MonoBehaviour
{
    public static SelectQuantity Instance { get; private set; }
    [SerializeField] InputField input;
    [SerializeField] Text description;
    [SerializeField] Button buttonYES, buttonNO;
    private Canvas canvas;


    private void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void Subscribe(string description, Action<int> callYES, Action callNO)
    {
        canvas.enabled = true;
        this.description.text = description;

        buttonYES.onClick.RemoveAllListeners();
        buttonYES.onClick.AddListener(()=> {
            int count;
            try { count = Int32.Parse(input.text); }
            catch { count = 1; }
            callYES?.Invoke(count);
            canvas.enabled = false; 
        });

        buttonNO.gameObject.SetActive(true);
        buttonNO.onClick.RemoveAllListeners();
        if (callNO != null)
            buttonNO.onClick.AddListener(() => { callNO?.Invoke(); canvas.enabled = false; }); 
        else
            buttonNO.gameObject.SetActive(false);
    }
}
