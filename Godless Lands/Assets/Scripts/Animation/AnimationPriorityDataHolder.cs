using Animation.Data;
using Protocol.Data.Replicated.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animation
{
    internal class AnimationPriorityDataHolder : MonoBehaviour
    {
        [SerializeField]
        private AnimationPriorityData _animationPriorityData;

        public int GetPriority(AnimationID animationID)
        {
            return _animationPriorityData.GetPriority(animationID);
        }
    }
}
