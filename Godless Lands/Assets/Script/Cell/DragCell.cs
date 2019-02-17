using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cells
{
    public class DragCell : MonoBehaviour
    {
        private CellParent cellParent;
        private PointerEventData m_PointerEventData;
        private EventSystem m_EventSystem;
        private Cell cell;


        public void CaptureItem(Transform parent, Cell cell, Vector3 position)
        {
            cellParent = parent.GetComponentInParent<CellParent>();
            m_EventSystem = EventSystem.current; 
            this.cell = cell;
            transform.SetParent(cellParent.parent);
            ((RectTransform)transform).position = Input.mousePosition + ((parent as RectTransform).position - position);
            Texture2D texture = cell.GetIcon();
            GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

            cell.HideIcon();//Скрыть иконку в ячейке на время перемещения

        }

        private void Update()
        {

            if (!Input.GetButton("MouseLeft"))
            {
                Destroy(gameObject);
                enabled = false;
                if(Ray())
                cell.ShowIcon();//Снова показать иконеку в ячейке
 
            }
        }

        private bool Ray()
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
                        resultCell.Put(cell);
                        return true;
                    }
                }
               
            }
            cell.Abort();
            return false;
        }
    }
}