using RUCP;
using RUCP.Handler;
using RUCP.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfessionManager : MonoBehaviour
{
    public Profession blacksmith;
    public Profession tanner;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        HandlersStorage.RegisterHandler(Types.ProfessionUpdate, ProfessionUpdate);
    }

    private void ProfessionUpdate(Packet nw)
    {
        ProfessionEnum profession = (ProfessionEnum)nw.ReadInt();
        switch (profession)
        {
            case ProfessionEnum.Blacksmith:
                blacksmith.UpdatePruf(nw);
                break;
            case ProfessionEnum.Tanner:
                tanner.UpdatePruf(nw);
                break;
            default:
                print("Error update profession");
                break;
        }
    }

    public void OpenClose()
    {
        canvas.enabled = !canvas.enabled;
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.ProfessionUpdate);
    }
}
