using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class Cell : MonoBehaviour
    {
 
        protected Image _icon;
        protected bool _locked = false;

        internal void Init()
        {
            _icon = transform.Find("Icon").GetComponent<Image>();
            Hide();
        }

        protected void Start()
        {
            transform.localScale = Vector3.one;
        }


        public virtual bool IsEmpty() { return true; }//Если ячейка пуста true

        public virtual void Use() { }//Использовать содержимое ячейки
        public virtual void Put(Cell cell) { }//Положить в ячейку содержимое другой ячейки
        public Texture2D GetIcon()//Возвращает иконку предмета\Умения
        {
           return _icon.sprite.texture;
        }
        public Sprite GetSprite()//Возвращает иконку предмета\Умения
        {
            return _icon.sprite;
        }

        public virtual bool IsInteractingWithCurrentCell(Cell cell) => true;

        public virtual void Hide()
        {
            if (_icon != null)
            {
                _icon.enabled = false;
            }
        }

        public virtual void Show()
        {
            if(!IsEmpty())
            if (_icon != null) _icon.enabled = true;
        }

        public virtual void Abort()
        {
        }

        public virtual string GetText()
        {
            return "";
        }

        /// <summary>
        /// Уникальный ИД содержимого ячейки
        /// </summary>
        /// <returns></returns>
        public virtual long GetItemUID()
        {
            return 0;
        }

        internal bool IsLocked() => _locked;
    }
}