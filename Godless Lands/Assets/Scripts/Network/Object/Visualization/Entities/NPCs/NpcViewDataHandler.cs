using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;
using Helpers;

namespace Network.Object.Visualization.Entities.NPC
{
    internal class NpcViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private NPCsFactory _npcFactory;
        private NpcSkinData _visualData;
        private TransfromDataHandler _networkTransform;
        private int _currentVisualObjectId;


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

        protected async void UpdateVisualObject(int visualObjectId)
        {
            _currentVisualObjectId = visualObjectId;

            var assetHolder = await _npcFactory.CreateNPC(_visualData.SkinID);

            if(_currentVisualObjectId != visualObjectId)
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
