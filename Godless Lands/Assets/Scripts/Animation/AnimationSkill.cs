using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSkill : MonoBehaviour {

    private Animator animator;
    public bool block = false;
    private float speedAnim = 1.0f;

    public float GetSpeedAnim()
    {
        return speedAnim;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /*public void UseAnim(string anim)
    {
        animator.Play(anim);
    }*/

    public void UseAnimationSkill(int animation, float speed)
    {
        if (block) return;
        speedAnim = speed;
        if (block) return;
        animator.SetInteger("AttackType", animation);
        animator.SetTrigger("atack");
    }

    public void UseAnimState(int animation)
    {
        if (block) return;
        if (animation == 2) { animator.SetTrigger("spell");  }
        else if (animation == 3) { animator.SetTrigger("react");  }
        else if (animation == 4) {  animator.SetTrigger("dying");  }
        else if (animation == 5) { animator.SetBool("dead", false); }
    }

    public void UseAnimState(int animation,float time)
    {
        if (block) return;
        speedAnim = time;
        animator.SetInteger("StateType", animation);
        animator.SetTrigger("State");
    }

    public void DeadOn()
    {
        block = true;
        if(animator == null) animator = GetComponent<Animator>();
        animator.SetTrigger("dying");
    }

    public void DeadOff()
    {
        block = false;
        if (animator == null) animator = GetComponent<Animator>();
        animator.SetTrigger("Cancel");
    }
}
