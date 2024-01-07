using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cells
{
    public class CellMove : MonoBehaviour
    {
        private Vector2 startMousePosition;
        private Vector3 startItemPosition;
        private RectTransform parent;
        private RectTransform rectTransform;
        private Camera _camera;

        void Start()
        {
            _camera = GetComponentInParent<Canvas>().worldCamera;
            rectTransform = GetComponent<RectTransform>();
            parent = transform.parent as RectTransform;
            transform.localScale = Vector3.one;
            startItemPosition = rectTransform.localPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, _camera, out startMousePosition);
        }

        private void Update()
        {


            Vector2 currentPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Input.mousePosition, _camera, out currentPosition))
            {
                Vector3 offsetToOriginal = currentPosition - startMousePosition;
                rectTransform.localPosition = startItemPosition + offsetToOriginal;
            }

        }
    }
}