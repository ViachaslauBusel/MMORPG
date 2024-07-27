using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization.Entities.Resources
{
    internal class MiningStoneSkinDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private ResourcesFactory _miningStonesFactory;
        private MiningStoneSkinData _visualData;
        private TransfromDataHandler _networkTransform;


        [Inject]
        private void Construct(ResourcesFactory miningStonesFactory)
        {
            _miningStonesFactory = miningStonesFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();
        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (MiningStoneSkinData)updatedData;

            UpdateVisualObject();
        }

        protected async void UpdateVisualObject()
        {
            int skinID = _visualData.SkinID;

            var assetHolder = await _miningStonesFactory.CreateStone(skinID);

            if (skinID != _visualData.SkinID)
            {
                assetHolder.Release();
                return;
            }
            DestroyExistingUnitObject();
            assetHolder.Instantiate(transform, _networkTransform.Position);
            SetVisualObject(assetHolder);
        }
    }
}
