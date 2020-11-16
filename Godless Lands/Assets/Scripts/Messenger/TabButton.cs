using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger {
    public class TabButton : MonoBehaviour
    {
        private Tab tab;
        private Button button;

        public Tab Tab => tab;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Select);
        }
        public void Set(Tab tab)
        {
            this.tab = tab;
            Text text = GetComponentInChildren<Text>();
            text.text = tab.name;
            RectTransform rectTransform = GetComponent<RectTransform>();
            TextGenerator textGen = new TextGenerator();
            TextGenerationSettings generationSettings = text.GetGenerationSettings(rectTransform.rect.size);
            float width = textGen.GetPreferredWidth(text.text, generationSettings) + 4.0f;
            if (width < 50) width = 50;
            Vector2 size = rectTransform.sizeDelta;
            size.x = width;
            rectTransform.sizeDelta = size;
        }

        public void Select()
        {
            tab.Select();
            TabsGroup.Instance.UpdateSelect();
        }

        internal void UpdateSelect()
        {
            button.interactable = !tab.IsSelected;
        }
    }
}