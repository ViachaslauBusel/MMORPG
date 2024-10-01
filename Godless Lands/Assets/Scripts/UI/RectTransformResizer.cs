using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public class RectTransformResizer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _targetRect;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            Debug.Log("Resizing");
            Vector2 size = _rectTransform.sizeDelta;
            size.y = _targetRect.sizeDelta.y;
            _rectTransform.sizeDelta = size;
        }
    }
}
