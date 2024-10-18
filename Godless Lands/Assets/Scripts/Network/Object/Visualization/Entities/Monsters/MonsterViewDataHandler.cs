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

        public int ID => _visualData.SkinID;

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

            //Debug.Log("MonsterViewDataHandler: UpdateData: " + _visualData.InNeedChaceVisual);
            UpdateVisualObject();
        }

        protected async void UpdateVisualObject()
        {
            if (_skinID == _visualData.SkinID) return;

            _skinID = _visualData.SkinID;
            int skinID = _visualData.SkinID;

            var assetHolder = await _monstersFactory.CreateMonsterAsync(skinID);

            if (skinID != _visualData.SkinID || assetHolder.IsValid == false)
            {
                assetHolder.Release();
                return;
            }

            DestroyExistingUnitObject();
            assetHolder.InstanceObject.transform.SetParent(transform);
            assetHolder.InstanceObject.transform.position = _networkTransform.Position;
            assetHolder.InstanceObject.transform.rotation = Quaternion.Euler(0, _networkTransform.Rotation, 0);
            SetVisualObject(assetHolder);
        }
    }
}
