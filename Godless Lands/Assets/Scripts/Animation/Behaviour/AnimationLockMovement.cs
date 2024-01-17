using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Animation.Behaviour
{
    internal class AnimationLockMovement : StateMachineBehaviour
    {
        private CharacterController m_characterController;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            m_characterController = animator.GetComponent<CharacterController>();
            m_characterController.enabled = false;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            m_characterController.enabled = true;
        }
    }
}
