using Animation;
using Protocol.Data.Replicated.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : StateMachineBehaviour
{
    [SerializeField]
    private AnimationID _animationID;
    [SerializeField]
    private string _multipler;
    private AnimationPlaybackTimeBuffer _animationPlaybackTimeBuffer;
    private float _startPlayTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animationPlaybackTimeBuffer = animator.GetComponent<AnimationPlaybackTimeBuffer>();

        if (_animationPlaybackTimeBuffer != null && _animationPlaybackTimeBuffer.TryPullTime(_animationID, out float playTime))
        {
            Debug.Log($"[{_animationID}] OnStateEnter: " + playTime);
            _startPlayTime = Time.time;
            float animationMultiplier = stateInfo.length / playTime;
            animator.SetFloat(_multipler, animator.GetFloat(_multipler) * animationMultiplier);
        }
        else animator.SetFloat(_multipler, 1f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"[{_animationID}] OnStateExit:{Time.time - _startPlayTime}");
    }
}
