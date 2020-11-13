using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkillsBar
{
    
    public class SkillsBarMove : EventTrigger
    {
        private RectTransform windowParent;
        private RectTransform window;
        //Позиция панели при нажатии
        private Vector2 originalLocalPointerPosition;
        private Vector3 originalPanelLocalPosition;

        private Vector2 minBorder;
        private Vector2 maxBorder;

        private void Awake()
        {
            window = transform.GetComponent<RectTransform>();
            windowParent = window.parent as RectTransform;
        }

        private void Start()
        {
            StartCoroutine(LateStart());
           
        }

        IEnumerator LateStart()
        {
            int cadr = 3;
            while (cadr-- >= 0) yield return 0;
            window.position = new Vector3(PlayerPrefs.GetFloat("SkillsBarX", window.position.x), PlayerPrefs.GetFloat("SkillsBarY", window.position.y), window.position.z);
            BorderControl();
        }

        public void SetBorder(float scale)
        {
            minBorder = new Vector2((window.sizeDelta.x / 2.0f) * scale, (window.sizeDelta.y / 2.0f) * scale);
            maxBorder = new Vector2(Screen.width - minBorder.x, Screen.height - minBorder.y);
            BorderControl();
        }

        public override void OnPointerDown(PointerEventData data)
        {
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
                BorderControl();
            }
        }

        public override void OnPointerUp(PointerEventData eventData)//Сохронение положение панели
        {
            PlayerPrefs.SetFloat("SkillsBarX", window.position.x);
            PlayerPrefs.SetFloat("SkillsBarY", window.position.y);
        }

        public void BorderControl()
        {
            if (window.position.x < minBorder.x) window.position = new Vector3(minBorder.x, window.position.y, window.position.z);
            else if(window.position.x > maxBorder.x) window.position = new Vector3(maxBorder.x, window.position.y, window.position.z);

            if (window.position.y < minBorder.y) window.position = new Vector3(window.position.x, minBorder.y, window.position.z);
            else if (window.position.y > maxBorder.y) window.position = new Vector3(window.position.x, maxBorder.y, window.position.z);
        }
    }
}