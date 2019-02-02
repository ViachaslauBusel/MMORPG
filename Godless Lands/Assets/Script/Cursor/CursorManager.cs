using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour {

    public Texture2D cursorStandart;
    public Texture2D cursorDrop;
    public Texture2D cursorAtack;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private Camera _camera;

    private void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Cursor.SetCursor(cursorStandart, hotSpot, cursorMode);
    }

    private void Update()
    {
        if (Input.GetButton("MouseRight"))
        {
            Cursor.visible = false;
            return;
        }
        Cursor.visible = true;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.SetCursor(cursorStandart, hotSpot, cursorMode);
            return;
        }

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        int layermask = 1 << 8;//Monsters
        layermask |= 1 << 10;//DeadMonsters
        if (Physics.Raycast(ray, out hit, layermask))
        {
            switch (hit.transform.gameObject.layer)
            {
                case 8://Monsters
                    Cursor.SetCursor(cursorAtack, hotSpot, cursorMode);
                    break;
                case 9://Players
                    Cursor.SetCursor(cursorAtack, hotSpot, cursorMode);
                    break;
                case 10://DeadMobs
                    Cursor.SetCursor(cursorDrop, hotSpot, cursorMode);
                    break;
                default:
                    Cursor.SetCursor(cursorStandart, hotSpot, cursorMode);
                    break;
            }
        }
    }

}
