using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimation : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float time = animator.GetComponent<AnimationSkill>().GetSpeedAnim();

        float speedUse = stateInfo.length / time;
        if (float.IsInfinity(speedUse))
        {
            Debug.Log("Время использование умения: " + time + " Множитель: " + speedUse + " длина анимации:" + stateInfo.length + " speed:" + stateInfo.speed);
        }
      //  Debug.Log("Время использование умения: " + time + " Множитель: " + speedUse + " длина анимации:" + stateInfo.length +" speed:"+ stateInfo.speed);
        animator.SetFloat("SpeedSkill", animator.GetFloat("SpeedSkill") * speedUse);
    }
}
