using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RUCP;
using System;
using RUCP.Packets;

public class HPView : MonoBehaviour {

    public Text name_txt;
    public Image hp_bar;
    public Text hp_txt;
    public Image mp_bar;
    public Text mp_txt;
    public Image stamina_bar;
    public Text stamina_txt;

    private void Awake()
    {
        HandlersStorage.RegisterHandler(Types.HPViewUpdate, HPViewUpdate);//byte 0-load name hp mp stamina/ 1 - hp mp stamina / 2 - hp/ 3 - mp/ 4 - stamina
    }

    private void HPViewUpdate(Packet nw)
    {
        int layer = nw.ReadByte();
        switch (layer)
        {
            case 0://load
                SetName(nw.ReadString());
                UpdateHP(nw);
                UpdateMP(nw);
                UpdateStamina(nw);
                break;
            case 1://update
                UpdateHP(nw);
                UpdateMP(nw);
                UpdateStamina(nw);
                break;
            case 2://updateHP  
                UpdateHP(nw);
                break;
            case 3:
                UpdateMP(nw);
                break;
            case 4:
                UpdateStamina(nw);
                break;
        }
        
    }

    public void SetName(string _name)
    {
        name_txt.text = _name;
    }

    public void UpdateHP(Packet nw)
    {
        int hp = nw.ReadInt();
        int max_hp = nw.ReadInt();
        hp_bar.fillAmount = hp / (float)max_hp;
        hp_txt.text = hp + "/" + max_hp;
    }
    public void UpdateMP(Packet nw)
    {
        int mp = nw.ReadInt();
        int maxMp = nw.ReadInt();
        mp_bar.fillAmount = mp / (float)maxMp;
        mp_txt.text = mp + "/" + maxMp;
    }
    public void UpdateStamina(Packet nw)
    {
        int stamina = nw.ReadInt();
        int maxStamina = nw.ReadInt();
        stamina_bar.fillAmount = stamina / (float)maxStamina;
        stamina_txt.text = stamina + "/" + maxStamina;
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.HPViewUpdate);
    }
}
