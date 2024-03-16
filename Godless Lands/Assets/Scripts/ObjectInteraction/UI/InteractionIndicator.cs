using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ObjectInteraction.UI
{
    public class InteractionIndicator : MonoBehaviour
    {

        [SerializeField]
        private Text _interactionText;

        internal void SetVisible(bool visible)
        {
            _interactionText.gameObject.SetActive(visible);
        }
    }
}
