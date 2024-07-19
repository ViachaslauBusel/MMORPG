using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization.Entities.DropBag
{
    internal class DropBagViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private DropBagFactory _dropBagFactory;
        private DropBagSkinData _visualData;
        private TransfromDataHandler _networkTransform;


        [Inject]
        private void Construct(DropBagFactory dropBagFactory)
        {
            _dropBagFactory = dropBagFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();

        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (DropBagSkinData)updatedData;

            UpdateVisualObject();
        }

        protected void UpdateVisualObject()
        {
            DestroyExistingUnitObject();

            GameObject visualObject =  CreateNewUnit();

            SetVisualObject(visualObject);
        }

        private GameObject CreateNewUnit()
        {
            return _dropBagFactory.CreateDropBag(_visualData.BagType, transform, _networkTransform.Position);
        }
    }
}
