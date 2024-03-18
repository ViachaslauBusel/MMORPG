using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cells
{
    public class Cell : MonoBehaviour
    {
 
        protected Image icon;


        protected void Awake()
        {
        
            icon = transform.Find("Icon").GetComponent<Image>();
            HideIcon();
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
           return icon.sprite.texture;
        }
        public Sprite GetSprite()//Возвращает иконку предмета\Умения
        {
            return icon.sprite;
        }

        public virtual void HideIcon()
        {
            if (icon != null)
            {
                icon.enabled = false;
            }
        }
        public virtual void ShowIcon()
        {
            if(!IsEmpty())
            icon.enabled = true;
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
    }
}