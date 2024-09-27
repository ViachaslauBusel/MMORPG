using Assets.Scripts.Units.Registry;
using Nickname;
using Protocol.Data.Units;
using Zenject;

namespace Units.Registry
{
    public class UnitVisualObject : BaseVisualObject
    {
       
        private int _unitId;
        private UnitType _unitType;
        private NicknameProviderService _nicknameProviderService;
        private string _nickname;

        public override int UnitId => _unitId;
        public override UnitType UnitType => _unitType;
        public override string Nickname => _nickname;


        [Inject]
        private void Construct(NicknameProviderService nicknameProviderService)
        {
            _nicknameProviderService = nicknameProviderService;
        }

        public void Initialize(int unitId, UnitType unitType)
        {
            _unitId = unitId;
            _unitType = unitType;
            _nickname = _nicknameProviderService.GetNickname(unitType, unitId);
        }
    }
}
