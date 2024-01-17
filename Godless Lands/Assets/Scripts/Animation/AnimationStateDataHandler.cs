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
    internal class AnimationStateDataHandler : MonoBehaviour, INetworkDataHandler
    {
        public AnimationStateID ActiveStateID { get; private set; }

        public event Action<AnimationStateID> OnAnimationStateChange;

        public void UpdateData(IReplicationData updatedData)
        {
            AnimationStateData data = (AnimationStateData)updatedData;
            ActiveStateID = data.AnimationStateID;
            OnAnimationStateChange?.Invoke(data.AnimationStateID);
        }
    }
}
