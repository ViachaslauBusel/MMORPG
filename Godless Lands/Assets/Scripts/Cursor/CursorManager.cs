using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour {

    public Texture2D cursorStandart;
    public Texture2D cursorDrop;
    public Texture2D cursorAtack;
    public Texture2D hammer;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private Camera _camera;
    private bool block = false;

    private void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Cursor.SetCursor(cursorStandart, hotSpot, cursorMode);
    }

    private void Update()
    {
        if (block) return;
        if (Input.GetButton("MouseRight"))
        {
            Cursor.visible = false;
            return;
        }
        Cursor.visible = true;
        if (EventSystem.current.IsPointerOverGameObject())//Если экран заграждает GUI
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
                case 14://DeadCharacter
                    //DisplayInformer.On(hit.transform.gameObject);
                    Cursor.SetCursor(cursorDrop, hotSpot, cursorMode);
                    return;
                case 10://DeadMobs
                    Cursor.SetCursor(cursorDrop, hotSpot, cursorMode);
                    break;
                default:
                    Cursor.SetCursor(cursorStandart, hotSpot, cursorMode);
                    break;
            }
        }
       // DisplayInformer.Off();
    }

    public void OnHammer()
    {
        Cursor.SetCursor(hammer, hotSpot, cursorMode);
        block = true;
    }
    public void OffHammer()
    {
        Cursor.SetCursor(cursorStandart, hotSpot, cursorMode);
        block = false;
    }

}
