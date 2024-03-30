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

namespace Assets.Scripts.NetworkObjectVisualization.Workbenches
{
    internal class WorkbenchViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private WorkbenchesFactory _workbenchesFactory;
        private WorkbenchSkinData _visualData;
        private TransfromDataHandler _networkTransform;


        [Inject]
        private void Construct(WorkbenchesFactory worbenchesFactory)
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

            SetVisualObject(visualObject);
        }

        private GameObject CreateNewUnit()
        {
            return _workbenchesFactory.CreateStone(_visualData.WorkbenchType, transform, _networkTransform.Position);
        }
    }
}
