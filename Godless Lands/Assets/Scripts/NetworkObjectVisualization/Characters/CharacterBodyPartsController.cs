using Protocol.Data.Replicated.Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization.Characters
{
    public class CharacterBodyPartsController : MonoBehaviour
    {
        [SerializeField]
        private BodyPart _weaponPart;
        [SerializeField]
        private BodyPart _pickaxePart;
        [SerializeField]
        private BodyPart _headPart;
      
        private CharacterMeshProviderService _meshProviderService;
        private Animator _animator;
        private int _weaponId;
        private int _headId;

        [Inject]
        private void Construct(CharacterMeshProviderService meshProviderService)
        {
            _meshProviderService = meshProviderService;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        internal void UpdateHead(int headId)
        {
            if (_headId == headId) return;

            _headId = headId;
        }

        internal void UpdateWeapon(int weaponId)
        {
            if (_weaponId == weaponId) return;

            _weaponId = weaponId;

            _animator.SetInteger("weaponType", _weaponId != 0 ? 1 : 0);

            UpdatePart(_weaponPart, _weaponId);
        }

        private void UpdatePart(BodyPart part, int partId)
        {
            part.ClearPart();

            GameObject partMesh = _meshProviderService.GetMesh(partId);

            if (partMesh == null)
            {
                Debug.Log($"[{part}][{partId}] CharacterBodyPartsController: UpdatePart: Part not found");
                return;
            }

            part.UpdatePart(partMesh);
        }
    }
}
