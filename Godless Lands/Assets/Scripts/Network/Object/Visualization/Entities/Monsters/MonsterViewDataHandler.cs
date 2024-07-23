using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization.Entities.Monsters
{
    internal class MonsterViewDataHandler : CachedNetworkObjectVisualHandler, INetworkDataHandler
    {
        private MonstersFactory _monstersFactory;
        private MonsterSkinData _visualData;
        private TransfromDataHandler _networkTransform;
        private int _skinID;

        protected override bool IsNeedChaceVisual => _visualData.InNeedChaceVisual;

        [Inject]
        private void Construct(MonstersFactory monstersFactory)
        {
            _monstersFactory = monstersFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();

        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (MonsterSkinData)updatedData;

            Debug.Log("MonsterViewDataHandler: UpdateData: " + _visualData.InNeedChaceVisual);
            UpdateVisualObject();
        }

        protected async void UpdateVisualObject()
        {
            if (_skinID == _visualData.SkinID) return;

            _skinID = _visualData.SkinID;
            int skinID = _visualData.SkinID;

            var assetHolder = await _monstersFactory.CreateMonster(skinID);

            if (skinID != _visualData.SkinID)
            {
                assetHolder.Release();
                return;
            }

            DestroyExistingUnitObject();
            assetHolder.Instantiate(transform, _networkTransform.Position, _networkTransform.Rotation);
            SetVisualObject(assetHolder);
        }
    }
}
