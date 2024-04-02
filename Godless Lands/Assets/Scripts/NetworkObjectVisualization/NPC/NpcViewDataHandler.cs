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

namespace NetworkObjectVisualization.NPC
{
    internal class NpcViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private NPCsFactory _npcFactory;
        private NpcSkinData _visualData;
        private TransfromDataHandler _networkTransform;


        [Inject]
        private void Construct(NPCsFactory monstersFactory)
        {
            _npcFactory = monstersFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();
        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (NpcSkinData)updatedData;

            UpdateVisualObject(_visualData.SkinID);
        }

        protected void UpdateVisualObject(int visualObjectId)
        {
            DestroyExistingUnitObject();

            GameObject visualObject = CreateNewUnit();

            SetVisualObject(visualObject);
        }

        private GameObject CreateNewUnit()
        {
            return _npcFactory.CreateNPC(_visualData.SkinID, transform, _networkTransform.Position, _networkTransform.Rotation);
        }
    }
}
