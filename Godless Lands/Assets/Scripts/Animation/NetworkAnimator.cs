using Protocol.Data.Replicated.Animation;
using UnityEngine;

namespace Animation
{
    public class NetworkAnimator : MonoBehaviour
    {
        private Animator m_animator;
        private AnimationPlaybackBufferHandler m_animationPlaybackBufferHandler;
        private float m_playbackTime = 1.0f;

        public float GetPlaybackTime()
        {
            return m_playbackTime;
        }

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_animationPlaybackBufferHandler = GetComponentInParent<AnimationPlaybackBufferHandler>();
        }

        private void OnEnable()
        {
            m_animationPlaybackBufferHandler.OnAnimationPlay += OnAnimationPlay;
        }

        private void OnDisable()
        {
            m_animationPlaybackBufferHandler.OnAnimationPlay -= OnAnimationPlay;
        }

        private void OnAnimationPlay(AnimationData data)
        {

            m_playbackTime = data.PlaybackTime != 0 ? data.PlaybackTime / 1_000f : 1.0f;

            m_animator.SetInteger("AttackType", (int)data.AnimationID);

            string triger = data.AnimationLayer switch
            {
                AnimationLayer.TimeAnimation => "atack",
                AnimationLayer.InstantAnimation => "skill",
                AnimationLayer.StateAnimation => "state",
                _ => null
            };

            if (triger == null)
            {
                Debug.LogError($"[{data.AnimationLayer}] Animation layer not found");
                return; 
            }
            m_animator.SetTrigger(triger);
            //m_animator.Play(data.AnimationName, data.Layer, data.NormalizedTime);
        }
    }
}
