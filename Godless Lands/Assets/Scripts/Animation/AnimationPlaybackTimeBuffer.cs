using Protocol.Data.Replicated.Animation;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    internal class AnimationPlaybackTimeBuffer : MonoBehaviour
    {
        private Dictionary<AnimationID, Queue<float>> _animationPlaybackTimeBuffer = new Dictionary<AnimationID, Queue<float>>();

        public void PushTime(AnimationID animationID, float playTime)
        {
            if (playTime < 0.01f) return;
            Debug.Log($"PushTime: {animationID} {playTime}");
            if (_animationPlaybackTimeBuffer.ContainsKey(animationID) == false)
            {
                _animationPlaybackTimeBuffer.Add(animationID, new Queue<float>());
            }

            _animationPlaybackTimeBuffer[animationID].Enqueue(playTime);
        }

        internal bool TryPullTime(AnimationID animationID, out float playTime)
        {
           if(_animationPlaybackTimeBuffer.ContainsKey(animationID) && _animationPlaybackTimeBuffer[animationID].Count > 0)
            {
                playTime = _animationPlaybackTimeBuffer[animationID].Dequeue();
                return true;
            }

            playTime = 0.0f;
            return false;
        }
    }
}
