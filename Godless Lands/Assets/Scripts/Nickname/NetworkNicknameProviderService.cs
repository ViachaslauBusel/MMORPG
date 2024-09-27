using Units.Registry;
using UnityEngine;

namespace Nickname
{
    public class NetworkNicknameProviderService
    {
        private readonly NicknameProviderService _nicknameProviderService;
        private readonly UnitVisualObjectRegistry _unitVisualObjectRegistry;

        public NetworkNicknameProviderService(NicknameProviderService nicknameProviderService, UnitVisualObjectRegistry unitVisualObjectRegistry)
        {
            _nicknameProviderService = nicknameProviderService;
            _unitVisualObjectRegistry = unitVisualObjectRegistry;
        }

        public string GetNickname(int networkId)
        {
            var networkObject = _unitVisualObjectRegistry.GetUnitVisualObjectByNetworkId(networkId);

            if(networkObject != null)
            {
                Debug.LogError("Network object is null");
                return string.Empty;
            }

            return _nicknameProviderService.GetNickname(networkObject.UnitType, networkObject.UnitId);
        }
    }
}
