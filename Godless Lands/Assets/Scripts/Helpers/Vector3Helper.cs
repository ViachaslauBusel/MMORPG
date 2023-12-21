using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class Vector3Helper
    {
        public static UnityEngine.Vector3 ToUnity(this System.Numerics.Vector3 vector)
        {
            return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
        }
        public static System.Numerics.Vector3 ToNumeric(this UnityEngine.Vector3 vector)
        {
            return new System.Numerics.Vector3(vector.x, vector.y, vector.z);
        }
    }
}
