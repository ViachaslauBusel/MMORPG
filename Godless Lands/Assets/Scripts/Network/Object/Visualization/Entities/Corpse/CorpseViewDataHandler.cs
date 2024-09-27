using AssetPerformanceToolkit.AssetManagement;
using Factories;
using Helpers;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Zenject;

namespace Network.Object.Visualization.Entities.Corpse
{
    internal class CorpseViewDataHandler : CachedNetworkObjectVisualHandler, INetworkDataHandler
    {
        private MonstersFactory _monstersFactory;
        private CorpseSkinData _visualData;
        private TransfromDataHandler _networkTransform;

        protected override bool IsNeedChaceVisual => false;

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
            _visualData = (CorpseSkinData)updatedData;

            UpdateVisualObject();
        }

        protected async void UpdateVisualObject()
        {
            AssetInstance visualObject = GetCachedVisualObject(_visualData.CachedObjectId);

            if (visualObject != null)
            {
                DestroyExistingUnitObject();
                SetVisualObject(visualObject);
                return;
            }

            int skinID = _visualData.SkinID;

            var assetHolder = await _monstersFactory.CreateMonsterAsync(skinID);

            if (skinID != _visualData.SkinID)
            {
                assetHolder.Release();
                return;
            }

            DestroyExistingUnitObject();
            assetHolder.InstanceObject.SetTransform(transform, _networkTransform.Position, _networkTransform.Rotation);
            SetVisualObject(assetHolder);
        }
    }
}
