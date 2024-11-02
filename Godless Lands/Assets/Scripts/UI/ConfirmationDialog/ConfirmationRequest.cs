using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.ConfirmationDialog
{
    public class ConfirmationRequest : IComparable<ConfirmationRequest>
    {
        private string _description;
        private Action _callYES;
        private Action _callNO;
        private float _endWaitTime;

        public int Priority => _endWaitTime > 0 ? 1 : 0;
        public string Description => _description;
        public Action CallYes => _callYES;
        public Action CallNo => _callNO;
        public float EndWaitTime => _endWaitTime;

        public ConfirmationRequest(string description, Action callYES, Action callNO, float waitTime)
        {
            _description = description;
            _callYES = callYES;
            _callNO = callNO;
            Debug.Log("Wait time: " + waitTime);
            if (waitTime > 0.001f) _endWaitTime = Time.time + waitTime;
        }

        public int CompareTo(ConfirmationRequest other)
        {
            if (Priority == other.Priority)
            {
                return other._endWaitTime.CompareTo(_endWaitTime);
            }
            return Priority > other.Priority ? 1 : -1;
        }
    }
}
