using Helpers;
using NetworkObjectVisualization;
using Protocol.Data.Replicated.Animation;
using System;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(AnimationPlaybackTimeBuffer))]
    public class NetworkAnimator : MonoBehaviour, IVisualObjectScript
    {
        private Animator _animator;
        private AnimationPlaybackBufferHandler _animationPlaybackBufferHandler;
        private AnimationStateDataHandler _animationStateDataHandler;
        private AnimationPlaybackTimeBuffer _animationPlaybackTimeBuffer;
        private float _playbackTime = 1.0f;
        private AnimationStateID _currentSata;

        public float GetPlaybackTime()
        {
            return _playbackTime;
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
            _animationPlaybackTimeBuffer.PushTime(data.AnimationID, _playbackTime);

            if (data.Direction != null)
            {
                Vector3 direction = data.Direction.Value.ToUnity();
                direction.y = 0;
                transform.LookAt(transform.position + direction);
            }

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
