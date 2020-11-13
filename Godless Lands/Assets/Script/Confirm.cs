using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Confirm : MonoBehaviour
{
    public static Confirm Instance { get; private set; }
    [SerializeField] Text description;
    [SerializeField] Button buttonYES, buttonNO;
    private Canvas canvas;



    private void Awake()
    {
        Instance = this;

        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void Subscribe(string description, Action callYES, Action callNO)
    {
        canvas.enabled = true;
        this.description.text = description;
        buttonYES.onClick.RemoveAllListeners();
        buttonYES.onClick.AddListener(() => { callYES?.Invoke(); canvas.enabled = false; });

        buttonNO.gameObject.SetActive(true);
        buttonNO.onClick.RemoveAllListeners();
        if (callNO != null)
            buttonNO.onClick.AddListener(() => { callNO?.Invoke(); canvas.enabled = false; });
        else
            buttonNO.gameObject.SetActive(false);
    }

}
