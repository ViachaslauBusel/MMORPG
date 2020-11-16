﻿using Cells;
using RUCP;
using RUCP.Handler;
using RUCP.Network;
using RUCP.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemRepair : MonoBehaviour
{
    private CursorManager cursorManager;
    private CellParent cellParent;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;

    private void Awake()
    {
        cursorManager = GameObject.Find("Cursor").GetComponent<CursorManager>();
        cellParent = transform.GetComponentInParent<CellParent>();
        m_EventSystem = EventSystem.current;
        HandlersStorage.RegisterHandler(Types.ItemRepair, Repair);
        enabled = false;
    }

    private void Repair(Packet nw)
    {
        switch (nw.ReadByte())
        {
            case 1://
                cursorManager.OnHammer();
                enabled = true;
                break;
            case 2://
                cursorManager.OffHammer();
                enabled = false;
                print("Off");
                break;
        }
    }

    private void Update()
    {
        if (Input.GetButton("MouseLeft"))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            foreach (GraphicRaycaster raycaster in cellParent.raycasters)
            {
                //Raycast using the Graphics Raycaster and mouse click position
                raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.tag.Equals("Cell"))
                    {
                        Cell resultCell = result.gameObject.GetComponent<Cell>();
                        if (resultCell == null) continue;
                        if (resultCell.GetType() == typeof(ItemCell) || resultCell.GetType() == typeof(ArmorCell))
                        {
                            ItemCell itemCell = resultCell as ItemCell;
                            RepairItem(itemCell.GetObjectID());
                            return;
                        }
                    }
                }

            }
            Exit();
            //   return false;
        }
    }
    public void Exit()
    {
        Packet nw = new Packet(Channel.Discard);
        nw.WriteType(Types.ItemRepair);
        nw.WriteByte((byte)2);//Закрыть интерфейс заточки
        NetworkManager.Send(nw);
    }

    public void RepairItem(int object_id)
    {
        Packet nw = new Packet(Channel.Discard);
        nw.WriteType(Types.ItemRepair);
        nw.WriteByte((byte)3);//Закрыть интерфейс заточки
        nw.WriteInt(object_id);
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.ItemRepair);
    }
}