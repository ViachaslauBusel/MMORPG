using System;
using UnityEngine;

namespace Units
{
    public class IKFootPlacement : MonoBehaviour
    {
        [SerializeField, Range(0, 1)]
        private float _footOffset = 0.1f;
        [SerializeField]
        private LayerMask _groundLayer;
        private Animator _animator;
        private CharacterController _characterController;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Debug.Log(_characterController.velocity.sqrMagnitude < 0.1f);
            {
                float offset = _characterController.velocity.sqrMagnitude < 0.1f ? 0.5f : 0f;
                SetFootIK(AvatarIKGoal.LeftFoot, offset);
                SetFootIK(AvatarIKGoal.RightFoot, offset);
            }
        }

        private void SetFootIK(AvatarIKGoal foot, float offset)
        {
            _animator.SetIKPositionWeight(foot, 1);
            _animator.SetIKRotationWeight(foot, 1);

            RaycastHit hit;
            if (Physics.Raycast(_animator.GetIKPosition(foot) + Vector3.up, Vector3.down, out hit, 1f+_footOffset+offset, _groundLayer))
            {
                _animator.SetIKPosition(foot, hit.point + Vector3.up * _footOffset);
                _animator.SetIKRotation(foot, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal));
            }
        }
    }
}
