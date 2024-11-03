using AssetPerformanceToolkit.AssetManagement;
using Protocol.MSG.Game.Equipment;
using UnityEngine;

namespace Network.Object.Visualization.Entities.Characters
{
    public class BodyPart : MonoBehaviour
    {
        [SerializeField]
        private bool _isVisible = true;
        [SerializeField]
        EquipmentType _equipmentType;
        private SkinnedMeshRenderer _defaultPart;
        private AssetInstance _part;
        private int _partID;
        private Skeleton _skeleton;

        public int PartID => _partID;
        public EquipmentType EquipmentType => _equipmentType;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                _part?.InstanceObject.SetActive(_isVisible);
            }
        }

        private void Awake()
        {
            _defaultPart = GetComponent<SkinnedMeshRenderer>();
            _skeleton = GetComponentInParent<Skeleton>();
        }

        internal void ToDefaultPart()
        {
            _part?.Release();
            _part = null;
            if (_defaultPart != null) 
                _defaultPart.enabled = true;
        }

        internal void SetPartID(int partId)
        {
            _partID = partId;
        }

        internal void UpdatePart(AssetInstance part)
        {
            _part = part;
            _part.InstanceObject.transform.SetParent(transform);
            _part.InstanceObject.transform.localPosition = Vector3.zero;
            _part.InstanceObject.transform.localRotation = Quaternion.identity;
            _part.InstanceObject.transform.localScale = Vector3.one;

            var datas = _part.InstanceObject.GetComponentsInChildren<BodyPartData>();
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    var newPart = data.GetComponent<SkinnedMeshRenderer>();
                    newPart.rootBone = _skeleton.RootBone;
                    newPart.bones = _skeleton.GetBones(data.Bones);
                }
            }

            _part.InstanceObject.SetActive(_isVisible);
            if (_defaultPart != null)
                _defaultPart.enabled = false;
        }
    }
}
