#if UNITY_EDITOR
using MonsterRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldEditor
{
    public class MonsterDrawGizmos : MonoBehaviour
    {
        private float _radius = 0.0f;
        public WorldMonster worldMonster;

        public float radius
        {
            set { _radius = value; }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(_radius, 1.6f, _radius));
        }
    }
}
#endif