using AssetPerformanceToolkit.AssetManagement;
using Protocol.MSG.Game.Equipment;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Network.Object.Visualization.Entities.Characters
{
    public class CharacterBodyPartsController : MonoBehaviour
    {
        private Dictionary<EquipmentType, BodyPart> _bodyParts = new Dictionary<EquipmentType, BodyPart>();
        private ItemPrefabProviderService _itemPrefabService;
        private Animator _animator;


        [Inject]
        private void Construct(ItemPrefabProviderService meshProviderService)
        {
            _itemPrefabService = meshProviderService;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            var parts = GetComponentsInChildren<BodyPart>();

            foreach (var part in parts)
            {
                _bodyParts.Add(part.EquipmentType, part);
            }
        }

        public BodyPart Get(EquipmentType type)
        {
            if (_bodyParts.TryGetValue(type, out BodyPart part) == false)
            {
                Debug.LogError($"CharacterBodyPartsController: Get: Part not found: {type}");
                return null;
            }

            return part;
        }

        public void UpdatePart(EquipmentType type, int partId)
        {
            Debug.Log($"CharacterBodyPartsController: UpdatePart: {type} {partId}");
            if (_bodyParts.TryGetValue(type, out BodyPart part) == false)
            {
                Debug.LogError($"CharacterBodyPartsController: UpdatePart: Part not found: {type}");
                return;
            }

            if (type == EquipmentType.WeaponRightHand)
            {
                _animator.SetInteger("weaponType", partId != 0 ? 1 : 0);
            }

            UpdatePart(part, _itemPrefabService.GetPrefab(partId), partId);
        }

        private async void UpdatePart(BodyPart part, AssetReference prefab, int partId)
        {
            if (partId == part.PartID) return;

            part.SetPartID(partId);

            if(prefab == null)
            {
                part.ToDefaultPart();
                return;
            }

            AssetInstance partMesh = await AssetLoader.LoadInstance(prefab);

            if (partMesh == null || partMesh.IsValid == false)
            {
                part.ToDefaultPart();
                partMesh?.Release();
                Debug.Log($"[{part}][{partId}] CharacterBodyPartsController: UpdatePart: Part not found");
                return;
            }

            // Check if the part is still the same
            if (part.PartID == partId)
            { part.UpdatePart(partMesh); }
        }
    }
}
