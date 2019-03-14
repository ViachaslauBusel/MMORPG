using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cells
{

    public class CellEvent : EventTrigger
    {
        private bool focus = false; //Индикатор наведен ли курсор на ячейку

        protected bool doubleClick = true;
        protected GameObject informer;
        private GameObject prefabDragCell;//Префаб премещаемого обьекта
        private float time_show = 1.0f; //Время через которое показывать информацию после наведение курсора
        private float timer;
        private float lastClick = 0.0f;//Время последнего нажатие на левую кнопку мыши
        protected Cell cell;
        protected CellParent cellParent;
        private Vector3 postionClick;//Позиция мыши во время нажатия на левую кнопку мыши

        protected void Start()
        {
            cellParent = GetComponentInParent<CellParent>();
            cell = GetComponent<Cell>();
            prefabDragCell = Resources.Load<GameObject>("Cell/DragCell");
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

        public virtual void ShowInfo() {
           // informer = Instantiate(prefabInformer);
        }

        public virtual void HideInfo()
        {
            if (informer == null) return;
            Destroy(informer);
        }


        public override void OnPointerClick(PointerEventData data)//Использвоние предмета
        {
            if (data.button == PointerEventData.InputButton.Left)//По двойному клику левой кнопки мышь
            {
                if (!doubleClick || (Time.time - lastClick) < 0.4f)
                {
                    cell.Use();
                }
                else
                {
                    lastClick = Time.time;
                }
            }
            else if (data.button == PointerEventData.InputButton.Right)//По клику правой кнопкай мыши
            {
                RightClick();
            }
        }

        public virtual void RightClick()
        {
            cell.Use();
        }

        public override void OnBeginDrag(PointerEventData eventData)//Перемещение предмета
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (cell.IsEmpty()) {  return; }
            DragCell dragCell =  Instantiate(prefabDragCell).GetComponent<DragCell>();
            dragCell.CaptureItem(transform, cell, postionClick);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) postionClick = Input.mousePosition;
        }
    }
}