using NodeEditor.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerSingleDialog : MonoBehaviour
{
    private Text _dialog;
    private Button _button;
    private DialogWindow _dialogWindow;

    [Inject]
    private void Construct(DialogWindow dialogWindow)
    {
        _dialogWindow = dialogWindow;
    }

    private void Awake()
    {
        _dialog = GetComponentInChildren<Text>();
        _button = GetComponentInChildren<Button>();
    }

    public void SetDialog(string dialog, Node nextNode)
    {
        _dialog.text = dialog;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => _dialogWindow.OpenDialog(nextNode));
    }
}
