using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class TransformHelper
    {
        public static void SetTransform(this GameObject obj, Transform parent, Vector3 worldPosition, float worldRotation)
        {
            obj.transform.SetParent(parent);
            obj.transform.position = worldPosition;
            obj.transform.rotation = Quaternion.Euler(0, worldRotation, 0);
        }
    }
}
