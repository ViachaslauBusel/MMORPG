using Quests.Journal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestJournalWindow : MonoBehaviour
{
    private Canvas m_canvas;

    private void Awake()
    {
        m_canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        m_canvas.enabled = true;
    }
    public void Close()
    {
        m_canvas.enabled = false;
    }
}
