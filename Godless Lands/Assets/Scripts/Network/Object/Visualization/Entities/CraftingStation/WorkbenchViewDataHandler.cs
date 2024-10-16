﻿using AssetPerformanceToolkit.AssetManagement;
using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Object.Visualization.Entities.Characters;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization.Entities.CraftingStation
{
    internal class WorkbenchViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private CraftingStationsFactory _workbenchesFactory;
        private WorkbenchSkinData _visualData;
        private TransfromDataHandler _networkTransform;


        [Inject]
        private void Construct(CraftingStationsFactory worbenchesFactory)
        {
            _workbenchesFactory = worbenchesFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();
        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (WorkbenchSkinData)updatedData;

            UpdateVisualObject();
        }

        protected void UpdateVisualObject()
        {
            DestroyExistingUnitObject();
            GameObject visualObject = CreateNewUnit();

            SetVisualObject(new AssetInstance(visualObject));
        }

        private GameObject CreateNewUnit()
        {
            return _workbenchesFactory.CreateStone(_visualData.WorkbenchType, transform, _networkTransform.Position);
        }
    }
}
