using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Animation;
using System;
using UnityEngine;

namespace Animation
{
    public class AnimationPlaybackBufferHandler : MonoBehaviour, INetworkDataHandler
    {
        public event Action<AnimationData> OnAnimationPlay;

        public void UpdateData(IReplicationData updatedData)
        {
           AnimationPlaybackBuffer buffer = (AnimationPlaybackBuffer)updatedData;

            foreach (var e in buffer.PlaybackHistory)
            {
                OnAnimationPlay?.Invoke(e);
            }
        }
    }
}
