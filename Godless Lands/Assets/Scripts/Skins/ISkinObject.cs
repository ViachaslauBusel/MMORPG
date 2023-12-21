using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Skins
{
    public interface ISkinObject
    {
        public GameObject SkinObject { get; }

        public event Action<GameObject> updateSkinObject;
    }
}
