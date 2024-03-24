using Factories;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization.Characters
{
    internal class CharacterViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private CharactersFactory _charatersFactory;
        private TransfromDataHandler _networkTransform;
        private CharacterBodyPartsController _bodyPartsController;

        [Inject]
        private void Construct(CharactersFactory charactersFactory)
        {
            _charatersFactory = charactersFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();
        }

        public void UpdateData(IReplicationData updatedData)
        {
            CharacterSkinData visualData = (CharacterSkinData)updatedData;

            if(_bodyPartsController == null)
            {
                GameObject characterObj = CreateNewUnit();
                _bodyPartsController = characterObj.GetComponent<CharacterBodyPartsController>();
                if(_bodyPartsController == null)
                {
                    Debug.LogError("CharacterViewDataHandler: UpdateData: CharacterBodyPartsController not found");
                    return;
                }
                SetVisualObject(characterObj);
            }
            _isNeedChaceVisual = visualData.InNeedChaceVisual;
            _bodyPartsController.UpdateWeapon(visualData.WeaponId);
            _bodyPartsController.UpdateHead(visualData.HeadId);
        }

        protected GameObject CreateNewUnit()
        {
            return _charatersFactory.CreateHumanMale(transform, _networkTransform.Position);
        }
    }
}
