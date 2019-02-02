using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkillsBar
{
    public class Tools : MonoBehaviour
    {
        public Texture2D activeIcon;
        public Texture2D deactiveIcon;
        private Image icon;
        private bool active;

        protected void Start()
        {
            icon = GetComponent<Image>();
        }

        protected bool activated
        {
            set {
                active = value;
                if (active)
                {
                    icon.sprite = Sprite.Create(activeIcon, new Rect(0.0f, 0.0f, activeIcon.width, activeIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
                    Activation();
                }
                else
                {
                    icon.sprite = Sprite.Create(deactiveIcon, new Rect(0.0f, 0.0f, deactiveIcon.width, deactiveIcon.height), new Vector2(0.5f, 0.5f), 100.0f);
                    Deactivation();
                }
            }
            get { return active; }
        }

        public void ButtonVoid()
        {
            activated = !active;
        }

        public virtual void Activation() { }//Вызывается при активации инструмента
        public virtual void Deactivation() { }//Вызывается при деактивации интсрумента
    }
}