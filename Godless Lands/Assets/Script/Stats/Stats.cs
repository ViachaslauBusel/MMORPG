using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stats : MonoBehaviour {

    public Text left_txt;
    public Text right_txt;
    private Canvas stats;
    private UISort uISort;


    private string char_name;
    private int physical_attack;
    public int prickingDamage;
    public int crushingDamage;
    public int choppingDamage;
    private int physical_defense;
    private float attack_speed;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.LoadStats, LoadingStats);
        RegisteredTypes.RegisterTypes(Types.UpdateStats, UpdateStats);
    }

    private void UpdateStats(NetworkWriter nw)
    {
        physical_attack = nw.ReadInt();
        prickingDamage = nw.ReadInt();
        crushingDamage = nw.ReadInt();
        choppingDamage = nw.ReadInt();
        physical_defense = nw.ReadInt();
        attack_speed = nw.ReadFloat();
        Redraw();
    }

    private void LoadingStats(NetworkWriter nw)
    {
        char_name = nw.ReadString();
        physical_attack = nw.ReadInt();
        prickingDamage = nw.ReadInt();
        crushingDamage = nw.ReadInt();
        choppingDamage = nw.ReadInt();
        physical_defense = nw.ReadInt();
        attack_speed = nw.ReadFloat();
        Redraw();
    }

    private void Redraw()
    {
        left_txt.text = "Имя - " + char_name + '\n'; //name

        left_txt.text += "Физ. Атака - " + physical_attack + '\n';//p. attack
        left_txt.text += "Колющий - " + prickingDamage + '\n';
        left_txt.text += "Дробящий - " + crushingDamage + '\n';
        left_txt.text += "Рубящий - " + choppingDamage + '\n';

        right_txt.text = '\n' + "Физ. Защита - " + physical_defense + '\n'; //p.def.

        left_txt.text += "Скор. Атаки - " + attack_speed.ToString("0.00") + '\n';  //attack speed
    }

    private void Start()
    {
        uISort = GetComponentInParent<UISort>();
        stats = GetComponent<Canvas>();
        stats.enabled = false;
    }

    public void OpenCloseStats()
    {
        stats.enabled = !stats.enabled;

        if (stats.enabled)
        {
            uISort.PickUp(stats);
        }
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.LoadStats);
        RegisteredTypes.UnregisterTypes(Types.UpdateStats);
    }
}
