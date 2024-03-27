using Equipment;
using NetworkObjectVisualization.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAnimation : StateMachineBehaviour
{
    private CharacterBodyPartsController _characterBodyPartsController;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (layerIndex == 0) return;
        _characterBodyPartsController = animator.GetComponent<CharacterBodyPartsController>();
        if(_characterBodyPartsController != null)
        {
            _characterBodyPartsController.Pickaxe.IsVisible = true;
            _characterBodyPartsController.Weapon.IsVisible = false;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(layerIndex == 0) return;
        if (_characterBodyPartsController != null)
        {
            _characterBodyPartsController.Pickaxe.IsVisible = false;
            _characterBodyPartsController.Weapon.IsVisible = true;
        }
    }
}
