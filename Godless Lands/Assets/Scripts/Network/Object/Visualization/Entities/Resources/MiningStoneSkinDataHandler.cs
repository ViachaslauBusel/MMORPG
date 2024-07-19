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
        private MiningStonesFactory _miningStonesFactory;
        private MiningStoneSkinData _visualData;
        private TransfromDataHandler _networkTransform;


        [Inject]
        private void Construct(MiningStonesFactory miningStonesFactory)
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

        protected void UpdateVisualObject()
        {
            DestroyExistingUnitObject();

            GameObject visualObject = CreateNewUnit();

            SetVisualObject(visualObject);
        }

        private GameObject CreateNewUnit()
        {
            return _miningStonesFactory.CreateStone(_visualData.SkinID, transform, _networkTransform.Position);
        }
    }
}
