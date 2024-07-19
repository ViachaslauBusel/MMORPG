using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization.Entities.Characters
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
            _bodyPartsController.UpdateWeapon(visualData.WeaponId);
            _bodyPartsController.UpdateTool(visualData.ToolId);
            _bodyPartsController.UpdateHead(visualData.HeadId);
        }

        protected GameObject CreateNewUnit()
        {
            return _charatersFactory.CreateHumanMale(transform, _networkTransform.Position);
        }
    }
}
