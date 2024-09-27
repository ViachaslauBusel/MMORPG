using Assets.Scripts.Units.Registry;
using Nickname;
using Protocol.Data.Units;
using UnityEngine;

namespace Units.Registry
{
    public class CharacterVisualObject : BaseVisualObject
    {
        private string _nickname;

        public override int UnitId => 0;
        public override UnitType UnitType => UnitType.Character;
        public override string Nickname => _nickname;

        public override void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            base.AttachToNetworkObject(networkObjectOwner);
            _nickname = networkObjectOwner.GetComponent<UnitNameHandler>().UnitName;
        }
    }
}
