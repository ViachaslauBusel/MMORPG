using Animation.Behaviour;
using Protocol.Data.Replicated.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Animation.Data
{
    [CreateAssetMenu(fileName = "AnimationPriorityData", menuName = "Data/AnimationPriorityData")]
    public class AnimationPriorityData : ScriptableObject
    {
        [SerializeField]
        private List<AnimationPriority> _animationPriorities = new List<AnimationPriority>();

        public IReadOnlyList<AnimationPriority> AnimationPriorities => _animationPriorities;

        public int GetPriority(AnimationID animationID)
        {
            AnimationPriority animationPriority = _animationPriorities.FirstOrDefault(x => x.AnimationID == animationID);
            return animationPriority != null ? animationPriority.Priority : 0;
        }

        public void UpdateData(List<AnimationPriority> animationPriorities)
        {
            _animationPriorities = animationPriorities;
        }
    }
}
