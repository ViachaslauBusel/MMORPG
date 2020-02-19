#if UNITY_EDITOR
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenWorldEditor
{
    public class ResourcesDrawGizmos : MonoBehaviour
    {
        private float _radius = 0.0f;
        public WorldFabric worldFabric;

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