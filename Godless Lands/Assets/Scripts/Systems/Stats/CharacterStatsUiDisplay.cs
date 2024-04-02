using Protocol.Data.Stats;
using RUCP;
using RUCP.Handler;
using System;
using Systems.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterStatsUiDisplay : MonoBehaviour 
{
    private CharacterStatsHolder m_stats;

    [SerializeField]
    private Text m_leftTxt;
    private Canvas m_canvas;
    private UISort m_uISort;


    [Inject]
    private void Construct(CharacterStatsHolder stats)
    {
        m_stats = stats;
    }

    private void Awake()
    {
        m_uISort = GetComponentInParent<UISort>();
        m_canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        m_canvas.enabled = false;
    }

    private void Redraw()
    {
        m_leftTxt.text = "Имя - " + m_stats.GetName() + '\n'; //name

        m_leftTxt.text += "Сила Атаки - " + m_stats.GetStat(StatCode.MinPattack) + " - " + m_stats.GetStat(StatCode.MaxPattack) + '\n';//p. attack


        m_leftTxt.text += "Физ. Защита - " + m_stats.GetStat(StatCode.PhysicalDefense) + '\n'; //p.def.

        m_leftTxt.text += "Скор. Атаки - " + m_stats.GetStat(StatCode.AttackSpeed) + '\n';  //attack speed
        m_leftTxt.text += "Скорость - " + m_stats.GetStat(StatCode.MoveSpeed) + '\n';
    }

    public void Open()
    {
        if (m_canvas.enabled) return;

        m_canvas.enabled = true;
        m_uISort.PickUp(m_canvas);
        m_stats.OnUpdateStats += Redraw;
        Redraw();
    }

    public void Close()
    {
        if (m_canvas.enabled == false) return;
        m_canvas.enabled = false;
        m_stats.OnUpdateStats -= Redraw;
    }

    public void OpenClose()
    {
        if(m_canvas.enabled)
        {
            Close();
        }
        else
        {
            Open();
        }   
    }
}
