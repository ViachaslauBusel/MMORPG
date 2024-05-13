﻿using Protocol.Data.Replicated.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animation.Data
{
    [Serializable]
    public class AnimationPriority
    {
        [SerializeField]
        private AnimationID _animationID;
        [SerializeField]
        private int _priority = 0;

        public AnimationID AnimationID => _animationID;
        public int Priority => _priority;

        public AnimationPriority(AnimationID animtion, int priority)
        {
            _animationID = animtion;
            _priority = priority;
        }

        internal void SetPriority(int priority)
        {
            _priority = priority;
        }
    }
}