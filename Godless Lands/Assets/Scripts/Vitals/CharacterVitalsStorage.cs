using Protocol.Data.Vitals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vitals
{
    public struct Vital
    {
        public VitalCode Code;
        public int CurrentValue;
        public int MaxValue;
    }

    public class CharacterVitalsStorage
    {
        private Dictionary<VitalCode, Vital> _vitals = new Dictionary<VitalCode, Vital>();

        public event Action VitalsUpdated;

        internal void NotifyVitalsUpdated() => VitalsUpdated?.Invoke();

        public void UpdateVital(VitalCode code, int currentValue, int maxValue)
        {
            if (_vitals.ContainsKey(code))
            {
                _vitals[code] = new Vital { Code = code, CurrentValue = currentValue, MaxValue = maxValue };
            }
            else
            {
                _vitals.Add(code, new Vital { Code = code, CurrentValue = currentValue, MaxValue = maxValue });
            }
        }

        public Vital GetVital(VitalCode code)
        {
            if (_vitals.ContainsKey(code))
            {
                return _vitals[code];
            }
            else
            {
                return new Vital { Code = code, CurrentValue = 0, MaxValue = 0 };
            }
        }
    }
}
