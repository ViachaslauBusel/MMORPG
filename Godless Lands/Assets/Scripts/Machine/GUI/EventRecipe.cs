using Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Machines;

public class EventRecipe : EventTrigger
{
    private bool focus = false; //Индикатор наведен ли курсор на ячейку
    private float time_show = 1.0f; //Время через которое показывать информацию после наведение курсора
    private float timer;
    protected GameObject informer;
    private RecipeComponent component;
    private Canvas parentCanvas;

    private void Awake()
    {
        component = GetComponentInParent<RecipeComponent>();
        
    }
    private void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public override void OnPointerEnter(PointerEventData data)//Срабатывает при наведении курсора
    {
        focus = true;
    }
    public override void OnPointerExit(PointerEventData data)//Сбрабатывает при выводе курсора
    {
        timer = time_show;
        focus = false;
        HideInfo();
    }
    private void Update()
    {
        if (focus)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)//Таймер показа информации о обьекте
            {
                focus = false;
                ShowInfo();
            }
        }
    }

    public virtual void ShowInfo()
    {
         informer = ItemCellEvent.ItemInfo(parentCanvas.transform, component.GetItem());
    }

    public virtual void HideInfo()
    {
        if (informer == null) return;
        Destroy(informer);
    }
}
