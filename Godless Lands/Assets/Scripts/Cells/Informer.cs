using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class Informer : MonoBehaviour
    {
        private Transform parent;
        private Vector3 position;
        private int cadr = 3;
        public Image icon;
        public Text description;

        public void Initial(Vector3 position, Transform parent)
        {
            this.parent = parent;
            transform.SetParent(parent);
            this.position = position;
            icon.enabled = false;
        }

        private void Start()
        {
            transform.localScale = Vector3.one;
        }

        // Update is called once per frame
        void Update()
        {
            if (--cadr < 0)
            {
                RectTransform rectTransform = GetComponent<RectTransform>();
                Vector2 _sizeDelta = rectTransform.sizeDelta * parent.localScale.x;

                //Сместить позицию так что бы угол был на позиции мыши в зовисмости от части экрана
                if (position.x > (Screen.width / 2)) position.x -= (_sizeDelta.x / 2);
                else position.x += (_sizeDelta.x / 2);
                if (position.y > (Screen.height / 2)) position.y -= (_sizeDelta.y / 2);
                else position.y += (_sizeDelta.y / 2);
                rectTransform.position = position;

                enabled = false;
                icon.enabled = true;
            }
        }

        public void SetIcon(Texture2D _icon)
        {
            icon.sprite = Sprite.Create(_icon, new Rect(0.0f, 0.0f, _icon.width, _icon.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}