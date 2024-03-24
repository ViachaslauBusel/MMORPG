using Factories;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization.MiningStones
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

            Debug.Log("MiningStoneSkinDataHandler: UpdateData: " + _visualData.InNeedChaceVisual);
            _isNeedChaceVisual = _visualData.InNeedChaceVisual;
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
