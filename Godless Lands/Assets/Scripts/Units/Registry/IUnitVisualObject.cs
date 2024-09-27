using Protocol.Data.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Units.Registry
{
    public interface IUnitVisualObject
    {
        public int NewtorkId { get; }
        public int UnitId { get; }
        public UnitType UnitType { get; }
        public string Nickname { get; }
        Transform Transform { get; }
    }
}
