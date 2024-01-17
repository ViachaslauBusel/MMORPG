using Protocol.Data.Replicated.Animation;
using System;
using UnityEngine;

namespace Animation
{
    public class NetworkAnimator : MonoBehaviour
    {
        private Animator m_animator;
        private AnimationPlaybackBufferHandler m_animationPlaybackBufferHandler;
        private AnimationStateDataHandler m_animationStateDataHandler;
        private float m_playbackTime = 1.0f;

        public float GetPlaybackTime()
        {
            return m_playbackTime;
        }

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animationPlaybackBufferHandler = GetComponentInParent<AnimationPlaybackBufferHandler>();
            m_animationStateDataHandler = GetComponentInParent<AnimationStateDataHandler>();
        }

        private void OnEnable()
        {
            m_animationPlaybackBufferHandler.OnAnimationPlay += OnAnimationPlay;
            m_animationStateDataHandler.OnAnimationStateChange += OnAnimationStateChange;
            OnAnimationStateChange(m_animationStateDataHandler.ActiveStateID);
        }

        private void OnDisable()
        {
            m_animationPlaybackBufferHandler.OnAnimationPlay -= OnAnimationPlay;
            m_animationStateDataHandler.OnAnimationStateChange -= OnAnimationStateChange;
        }

        private void OnAnimationPlay(AnimationData data)
        {

            m_playbackTime = data.PlaybackTime != 0 ? data.PlaybackTime / 1_000f : 1.0f;

            switch (data.AnimationLayer)
            {
                case AnimationLayer.TimeAnimation:
                    m_animator.SetInteger("AttackType", (int)data.AnimationID);
                    m_animator.SetTrigger("atack");
                    break;
                case AnimationLayer.InstantAnimation:
                    m_animator.SetInteger("AttackType", (int)data.AnimationID);
                    m_animator.SetTrigger("skill");
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
            m_animator.SetInteger("stateIndex", (int)state);
            if (state != AnimationStateID.None)
            { m_animator.SetTrigger("State"); }
        }
    }
}
