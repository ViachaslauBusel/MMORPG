using Network.Object.Visualization;
using UnityEngine;

namespace CombatMode
{
    public class WeaponAttachmentSwitcher : MonoBehaviour, IVisualObjectScript
    {
        [SerializeField]
        private Transform _weaponPoint;
        [SerializeField]
        private Transform _handPoint;
        [SerializeField]
        private Transform _backPoint;
        private CombatModeDataHandler _combatMode;

        public void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            _combatMode = networkObjectOwner.GetComponent<CombatModeDataHandler>();
            SwitchWeaponAttachment();
        }

        public void DetachFromNetworkObject()
        {
            _combatMode = null;
        }

        /// <summary>
        /// This method is called from the animation event.
        /// </summary>
        public void SwitchWeaponAttachment()
        {
            if (_combatMode == null)
            {
                Debug.LogError("CombatModeController is not set.");
                return;
            }

            Debug.Log($"Switching weapon attachment.:{_combatMode.CombatModeActive}");
            _weaponPoint.SetParent(_combatMode.CombatModeActive ? _handPoint : _backPoint);
            _weaponPoint.localPosition = Vector3.zero;
            _weaponPoint.localRotation = Quaternion.identity;
        }
    }
}
