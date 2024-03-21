using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Target
{
    public interface ITargetObject
    {
        public int ID { get; }
        Transform Transform { get; }
    }
}
