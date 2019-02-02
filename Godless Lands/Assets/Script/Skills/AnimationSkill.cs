using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSkill : MonoBehaviour {

    private Animator animator;
    private bool skill_block = false;
    public bool dead = false;
    private float speedAnim;

    public float GetSpeedAnim()
    {
        return speedAnim;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UseAnim(string anim)
    {
        animator.Play(anim);
    }

    public void UseAnimationSkill(int animation, float speed)
    {
        speedAnim = speed;
        if (dead) return;
        animator.SetInteger("AttackType", animation);
        animator.SetTrigger("atack");
    }

    public void UseAnimState(int animation)
    {
        if (animation == 2) { animator.SetTrigger("spell"); skill_block = true; }
        else if (animation == 3) { animator.SetTrigger("react"); skill_block = true; }
        else if (animation == 4) { animator.SetBool("dead", true); animator.SetTrigger("dying"); PlayerController controller = GetComponent<PlayerController>(); if (controller != null) controller.enabled = false; skill_block = true; }
        else if (animation == 5) { animator.SetBool("dead", false); }
    }

    public void freezeOn()
    {
       // if (animator.GetFloat("skill_speed") < 0.0f) animator.SetFloat("skill_speed", 1.0f);
    }

    public void FreexeOff()
    {
       // if(skill_block) animator.SetFloat("skill_speed", -1.0f);
    }
}
