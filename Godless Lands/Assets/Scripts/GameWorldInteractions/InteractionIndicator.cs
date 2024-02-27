using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameWorldInteractions
{
    public class InteractionIndicator : MonoBehaviour
    {

        [SerializeField]
        private Text _interactionText;

        //public void Show()
        //{
        //    _interactionText.gameObject.SetActive(true);
        //}

        //public void Hide()
        //{
        //    _interactionText.gameObject.SetActive(false);
        //}

        internal void SetVisible(bool visible)
        {
            _interactionText.gameObject.SetActive(visible);
        }
    }
}
