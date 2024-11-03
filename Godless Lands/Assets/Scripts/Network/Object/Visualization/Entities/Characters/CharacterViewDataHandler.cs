using AssetPerformanceToolkit.AssetManagement;
using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Protocol.MSG.Game.Equipment;
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
                SetVisualObject(new AssetInstance(characterObj));
            }
            _bodyPartsController.UpdatePart(EquipmentType.WeaponRightHand, visualData.WeaponId);
            _bodyPartsController.UpdatePart(EquipmentType.PickaxeTool, visualData.ToolId);
            _bodyPartsController.UpdatePart(EquipmentType.ArmorHead, visualData.HeadId);
            _bodyPartsController.UpdatePart(EquipmentType.ArmorChest, visualData.ChestId);
            _bodyPartsController.UpdatePart(EquipmentType.ArmorHands, visualData.HandsId);
            _bodyPartsController.UpdatePart(EquipmentType.ArmorLegs, visualData.LegsId);
            _bodyPartsController.UpdatePart(EquipmentType.ArmorFeet, visualData.FeetId);
        }

        protected GameObject CreateNewUnit()
        {
            return _charatersFactory.CreateHumanMale(transform, _networkTransform.Position);
        }
    }
}
