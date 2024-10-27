using Drop.GroundDrop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Cells
{
    public class DragCell : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _groundLayer;
        private GroundDropInputHandler _groundDropInputHandler;
        private CellParent _cellParent;
        private PointerEventData _PointerEventData;
        private EventSystem _EventSystem;
        private Cell _cell;


        [Inject]
        private void Construct(GroundDropInputHandler groundDropInputHandler)
        {
            _groundDropInputHandler = groundDropInputHandler;
        }

        public void CaptureItem(Transform parent, Cell cell, Vector3 position)
        {
            _cellParent = parent.GetComponentInParent<CellParent>();
            _EventSystem = EventSystem.current; 
            this._cell = cell;
            transform.SetParent(_cellParent.parent);
            ((RectTransform)transform).position = Input.mousePosition + ((parent as RectTransform).position - position);
            Texture2D texture = cell.GetIcon();
            GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

            //cell.Hide();//Скрыть иконку в ячейке на время перемещения

        }

        private void Update()
        {

            if (!Input.GetButton("MouseLeft"))
            {
                Destroy(gameObject);
                enabled = false;
                Ray();
              
            }
        }

        private void Ray()
        {
            //Set up the new Pointer Event
            _PointerEventData = new PointerEventData(_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            _PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            Result raycastResult = null;
            bool tryGroundRay = true;
            foreach (GraphicRaycaster raycaster in _cellParent.raycasters)
            {
                //Raycast using the Graphics Raycaster and mouse click position
                raycaster.Raycast(_PointerEventData, results);

                tryGroundRay = tryGroundRay && results.Count == 0;

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.tag.Equals("Cell"))
                    {
                        if (raycastResult == null)
                        {
                            raycastResult = new Result();
                            raycastResult.resultObj = result.gameObject;
                            raycastResult.order = result.sortingOrder;
                        }
                        else
                        {
                            if(result.sortingOrder > raycastResult.order)
                            {
                                raycastResult.resultObj = result.gameObject;
                                raycastResult.order = result.sortingOrder;
                            }
                        }
                        
                      //  return;
                    }
                }
               
            }
            if(raycastResult != null)
            {
                Cell resultCell = raycastResult.resultObj.GetComponent<Cell>();
                if(resultCell.IsInteractingWithCurrentCell(_cell))
                   resultCell.Put(_cell);
            }
            else _cell.Abort();

            if(tryGroundRay && _cell is ItemCell itemCell)
            {
                //Raycast ray from camera to ground and get hit point
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.MaxValue, _groundLayer))
                {
                    _groundDropInputHandler.DropItem(itemCell.GetItem(), hit.point);
                }
            }
         //   return false;
        }

        private class Result
        {
            public GameObject resultObj;
            public int order;
        }
    }
}