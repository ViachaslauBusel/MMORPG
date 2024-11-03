using Network.Object.Visualization.Entities.Characters;
using Protocol.MSG.Game.Equipment;
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
            _characterBodyPartsController.Get(EquipmentType.PickaxeTool).IsVisible = true;
            //_characterBodyPartsController.Get(EquipmentType.WeaponLeftHand).IsVisible = false;
            _characterBodyPartsController.Get(EquipmentType.WeaponRightHand).IsVisible = false;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(layerIndex == 0) return;
        if (_characterBodyPartsController != null)
        {
            _characterBodyPartsController.Get(EquipmentType.PickaxeTool).IsVisible = false;
            //_characterBodyPartsController.Get(EquipmentType.WeaponLeftHand).IsVisible = true;
            _characterBodyPartsController.Get(EquipmentType.WeaponRightHand).IsVisible = true;
        }
    }
}
