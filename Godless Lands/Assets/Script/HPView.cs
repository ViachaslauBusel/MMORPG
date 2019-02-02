using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RUCP;
using System;

public class HPView : MonoBehaviour {

    public Text name_txt;
    public Image hp_bar;
    public Text hp_txt;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.HPViewUpdateHP, HPViewUpdateHP);
    }

    private void HPViewUpdateHP(NetworkWriter nw)
    {
        int hp = nw.ReadInt();
        int max_hp = nw.ReadInt();

        UpdateHP(hp, max_hp);
    }

    public void SetName(string _name)
    {
        name_txt.text = _name;
    }

    public void UpdateHP(float hp, float max_hp)
    {
        hp_bar.fillAmount = hp / max_hp;
        hp_txt.text = hp + "/" + max_hp;
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.HPViewUpdateHP);
    }
}
