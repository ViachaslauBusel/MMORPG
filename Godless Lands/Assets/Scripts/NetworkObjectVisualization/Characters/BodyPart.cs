using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NetworkObjectVisualization.Characters
{
    public class BodyPart : MonoBehaviour
    {
        [SerializeField]
        private bool _isVisible = true;
        private GameObject _part;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                if (_part != null)
                {
                    _part.SetActive(_isVisible);
                }
            }
        }

        internal void ClearPart()
        {
            Destroy(_part);
        }

        internal void UpdatePart(GameObject weapon)
        {
            _part = Instantiate(weapon, transform);
            _part.SetActive(_isVisible);
        }
    }
}
