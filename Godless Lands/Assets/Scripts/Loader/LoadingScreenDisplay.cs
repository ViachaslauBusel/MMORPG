using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Loader
{
    public class LoadingScreenDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image m_progressBar;
        private Canvas m_canvas;


        private void Awake()
        {
            m_canvas = GetComponent<Canvas>();
            m_canvas.enabled = false;
        }

        internal void Show()
        {
            m_canvas.enabled = true;
        }

        public void Show(float percent)
        {
            Show();
            m_progressBar.fillAmount = Mathf.Clamp01(percent);
        }

        public void Hide()
        {
            m_canvas.enabled = false;
        }

        internal void Show(object value)
        {
            throw new NotImplementedException();
        }
    }
}
