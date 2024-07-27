using Animation.Data;
using Helpers;
using Network.Object.Visualization;
using Protocol.Data.Replicated.Animation;
using UnityEngine;
using Zenject;

namespace Animation
{
    [RequireComponent(typeof(AnimationPlaybackTimeBuffer))]
    public class NetworkAnimator : MonoBehaviour, IVisualObjectScript
    {
        private Animator _animator;
        private AnimationPlaybackBufferHandler _animationPlaybackBufferHandler;
        private AnimationStateDataHandler _animationStateDataHandler;
        private AnimationPlaybackTimeBuffer _animationPlaybackTimeBuffer;
        private AnimationPriorityData _animationPriorityData;
        private AnimationStateID _currentSata;


        [Inject]
        private void Construct(AnimationPriorityData animationPriorityData)
        {
            _animationPriorityData = animationPriorityData;
        }

        public void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            _animationPlaybackBufferHandler = networkObjectOwner.GetComponent<AnimationPlaybackBufferHandler>();
            _animationStateDataHandler = networkObjectOwner.GetComponent<AnimationStateDataHandler>();

            if (_animationPlaybackBufferHandler != null)
            {
                _animationPlaybackBufferHandler.OnAnimationPlay += OnAnimationPlay;
            }

            if(_animationStateDataHandler != null)
            {
                _animationStateDataHandler.OnAnimationStateChange += OnAnimationStateChange;
                if (_animationStateDataHandler.IsInitialized)
                { OnAnimationStateChange(_animationStateDataHandler.ActiveStateID); }
            }
        }

        public void DetachFromNetworkObject()
        {
            if (_animationPlaybackBufferHandler != null)
            {
                _animationPlaybackBufferHandler.OnAnimationPlay -= OnAnimationPlay;
                _animationPlaybackBufferHandler = null;
            }
            if (_animationStateDataHandler != null)
            {
                _animationStateDataHandler.OnAnimationStateChange -= OnAnimationStateChange;
                _animationStateDataHandler = null;
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animationPlaybackTimeBuffer = GetComponent<AnimationPlaybackTimeBuffer>();
        }

        private void OnAnimationPlay(AnimationData data)
        {
            _animationPlaybackTimeBuffer.PushTime(data.AnimationID, data.PlaybackTime / 1_000f);

            if (data.Direction != null)
            {
                Vector3 direction = data.Direction.Value.ToUnity();
                direction.y = 0;
                transform.LookAt(transform.position + direction);
            }

            int currentPriority = _animator.GetInteger("Priority");
            int newPriority = _animationPriorityData.GetPriority(data.AnimationID);
            if (newPriority < currentPriority)
            {
                Debug.Log($"[{data.AnimationID}] Priority is lower than current priority");
                return;
            }
            _animator.SetInteger("Priority", newPriority);

            switch (data.AnimationLayer)
            {
                case AnimationLayer.TimeAnimation:
                case AnimationLayer.InstantAnimation:
                    _animator.SetInteger("AnimationType", (int)data.AnimationID);
                    _animator.SetTrigger("TriggerAnimation");
                    break;
                case AnimationLayer.StateAnimation:
                  
                    break;
                default:
                    Debug.LogError($"[{data.AnimationLayer}] Animation layer not found");
                    return;
            }
            //m_animator.Play(data.AnimationName, data.Layer, data.NormalizedTime);
        }

        private void OnAnimationStateChange(AnimationStateID state)
        {
            Debug.Log($"OnAnimationStateChange: {state}");

            _animator.SetInteger("stateIndex", (int)state);
            if (state != AnimationStateID.None && _currentSata != state)
            { _animator.SetTrigger("State"); }
            _currentSata = state;
        }
    }
}
