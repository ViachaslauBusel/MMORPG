using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Animation;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
