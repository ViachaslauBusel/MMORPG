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
        private GameObject _part;

        internal void ClearPart()
        {
            Destroy(_part);
        }

        internal void UpdatePart(GameObject weapon)
        {
            _part = Instantiate(weapon, transform);
        }
    }
}
