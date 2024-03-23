using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAnimation : StateMachineBehaviour
{
    //private Armor armor;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //armor = animator.GetComponent<Armor>();
       // armor.OnPickaxe();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
       // armor.OffPickaxe();
    }
}
