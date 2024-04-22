using Factories;
using NetworkObjectVisualization;
using Protocol.Data.Replicated.Skins;
using Protocol.Data.Replicated;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization.DropBag
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
