using NodeEditor.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerDoubleDialog : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_firstDialog, m_secondDialog;
    [SerializeField]
    private Button m_firstButton, m_secondButton;
    private DialogWindow m_dialogWindow;

    [Inject]
    private void Construct(DialogWindow dialogWindow)
    {
        m_dialogWindow = dialogWindow;
    }

    public bool IsEmptyFirstDialog => string.IsNullOrEmpty(m_firstDialog.text);
    public bool IsEmptySecondDialog => string.IsNullOrEmpty(m_secondDialog.text);

    private void SetFirstDialog(string dialog, Node nextNode)
    {
        m_firstDialog.text = dialog;
        m_firstButton.onClick.RemoveAllListeners();
        m_firstButton.onClick.AddListener(() => m_dialogWindow.OpenDialog(nextNode));
    }
    private void SetSecondDialog(string dialog, Node nextNode) 
    {
        m_secondDialog.text = dialog;
        m_secondButton.onClick.RemoveAllListeners();
        m_secondButton.onClick.AddListener(() => m_dialogWindow.OpenDialog(nextNode));
    }

    internal void AddDialogue(string replica, Node nextNode)
    {
        if (IsEmptyFirstDialog) SetFirstDialog(replica, nextNode);
        else if (IsEmptySecondDialog) SetSecondDialog(replica, nextNode);
        else Debug.LogError("PlayerDoubleDialog: Error, all replicas have already been initialized");

    }
}
