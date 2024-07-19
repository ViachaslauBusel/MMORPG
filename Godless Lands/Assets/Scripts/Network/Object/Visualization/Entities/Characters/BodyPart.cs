using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Network.Object.Visualization.Entities.Characters
{
    public class BodyPart : MonoBehaviour
    {
        [SerializeField]
        private bool _isVisible = true;
        private MeshHolder _part;
        private int _partID;

        public int PartID => _partID;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                _part?.SetActiveInstance(_isVisible);
            }
        }

        internal void ClearPart()
        {
            _part?.Release();
        }

        internal void SetPartID(int partId)
        {
            _partID = partId;
        }

        internal void UpdatePart(MeshHolder weapon)
        {
            _part = weapon;
            _part.Instantiate(transform);
            _part.SetActiveInstance(_isVisible);
        }
    }
}
