﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimation : StateMachineBehaviour
{
    private float animTimer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animTimer = animator.GetComponent<AnimationSkill>().GetSpeedAnim();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animTimer -= Time.deltaTime;
        if(animTimer <= 0.0f)
        {
            animator.SetTrigger("Cancel");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }
}
