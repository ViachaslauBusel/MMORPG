using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public static class NumberUtils
    {

        /// <summary>
        /// Returns a negative number if X < Y, 0 if X == Y, if X > Y returns a positive number
        /// </summary>
        /// <returns></returns>
        public static int CompareByte(int x, int y)
        {
            Debug.Assert(x <= byte.MaxValue & x >= 0, $"NumberUtils: Parameter X:{x} has an invalid value");
            Debug.Assert(y <= byte.MaxValue & y >= 0, $"NumberUtils: Parameter Y:{y} has an invalid value");
            return (x - y + byte.MaxValue + byte.MaxValue/2) % byte.MaxValue - byte.MaxValue / 2;
        }
    }
}
