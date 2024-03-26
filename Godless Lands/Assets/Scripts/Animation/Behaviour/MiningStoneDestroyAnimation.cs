using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animation.Behaviour
{
    internal class MiningStoneDestroyAnimation : MonoBehaviour
    {
        [SerializeField]
        private GameObject _lod;
        [SerializeField]
        private GameObject _fragments;
        [SerializeField]
        private Collider _collider;

        public void OnStartPlatDestroyAnimation()
        {
            _collider.enabled = false;
            _lod.SetActive(false);
            _fragments.SetActive(true);
        }
    }
}
