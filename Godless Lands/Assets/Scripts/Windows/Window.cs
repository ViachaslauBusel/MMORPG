﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Windows
{
    public class Window : MonoBehaviour
    {
        private Canvas _canvas;
        private UISort _uISort;

        public bool IsOpened => _canvas?.enabled ?? false;

        public event Action OnOpen;
        public event Action OnClose;

        protected virtual void Awake()
        {
            _uISort = GetComponentInParent<UISort>();
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        public virtual void Open()
        {
            _canvas.enabled = true;
            _uISort.PickUp(_canvas);
            OnOpen?.Invoke();
        }

        public virtual void Close()
        {
            _canvas.enabled = false;
            OnClose?.Invoke();
        }

        public void ToggleWindow()
        {
            if(IsOpened)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }
}
