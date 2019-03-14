using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellMenu : EventTrigger
{
    public Button use;
    public Button move;

    public void Initial(Transform parent)
    {
        transform.SetParent(parent);
        //  RectTransform rectTransform = GetComponent<RectTransform>();
        transform.position = Input.mousePosition;
        transform.localScale = Vector3.one;
    }
    public override void OnPointerExit(PointerEventData data)//Сбрабатывает при выводе курсора
    {
        Destroy(gameObject);
    }

    public void Use()
    {
        Destroy(gameObject);
    }
}
