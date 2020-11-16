using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowMove : EventTrigger
{
    private RectTransform windowParent;
    private RectTransform window;

    private UISort uISort;
    private Canvas canvas;
   // private Camera _camera;

    private void Start()
    {
     //   _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GetComponentInParent<Canvas>();
        uISort = GetComponentInParent<UISort>();
        window = transform.parent.GetComponent<RectTransform>();
        windowParent = window.parent as RectTransform;
    }

    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;



    public override void OnPointerDown(PointerEventData data)
    {
        uISort.PickUp(canvas);
        originalPanelLocalPosition = window.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(windowParent, data.position, data.pressEventCamera, out originalLocalPointerPosition);
    }

    public override void OnDrag(PointerEventData data)
    {
        if (window == null || windowParent == null || data.button != PointerEventData.InputButton.Left)
            return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(windowParent, data.position, data.pressEventCamera, out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
            window.localPosition = originalPanelLocalPosition + offsetToOriginal;
        }

    }


}
