using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectQuantityWindow : MonoBehaviour
{
    [SerializeField]
    private InputField _inputField;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private Button _buttonYES, _buttonNO;
    private Canvas _canvas;


    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    public void Subscribe(string description, Action<int> callYES, Action callNO)
    {
        _canvas.enabled = true;
        this._description.text = description;

        _buttonYES.onClick.RemoveAllListeners();
        _buttonYES.onClick.AddListener(()=> {
            int count = ParseInputField();
            callYES?.Invoke(count);
            _canvas.enabled = false; 
        });

        _buttonNO.gameObject.SetActive(true);
        _buttonNO.onClick.RemoveAllListeners();
        if (callNO != null)
            _buttonNO.onClick.AddListener(() => { callNO?.Invoke(); _canvas.enabled = false; }); 
        else
            _buttonNO.gameObject.SetActive(false);
    }

    private int ParseInputField()
    {
        if (int.TryParse(_inputField.text, out int count))
        {
            return count;
        }
        return 1;
    }
}
