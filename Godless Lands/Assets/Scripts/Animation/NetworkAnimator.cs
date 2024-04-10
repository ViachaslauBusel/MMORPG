using Helpers;
using NetworkObjectVisualization;
using Protocol.Data.Replicated.Animation;
using System;
using UnityEngine;

namespace Animation
{
    public class NetworkAnimator : MonoBehaviour, IVisualObjectScript
    {
        private Animator _animator;
        private AnimationPlaybackBufferHandler _animationPlaybackBufferHandler;
        private AnimationStateDataHandler _animationStateDataHandler;
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

            if(_animationPlaybackBufferHandler != null)
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
        }

        private void OnAnimationPlay(AnimationData data)
        {
            _playbackTime = data.PlaybackTime != 0 ? (data.PlaybackTime / 1_000f) : 1.0f;

            if(data.Direction != null)
            {
                Vector3 direction = data.Direction.Value.ToUnity();
                direction.y = 0;
                transform.LookAt(transform.position + direction);
            }

            switch (data.AnimationLayer)
            {
                case AnimationLayer.TimeAnimation:
                    _animator.SetInteger("AttackType", (int)data.AnimationID);
                    _animator.SetTrigger("atack");
                    break;
                case AnimationLayer.InstantAnimation:
                    _animator.SetInteger("AttackType", (int)data.AnimationID);
                    _animator.SetTrigger("skill");
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
