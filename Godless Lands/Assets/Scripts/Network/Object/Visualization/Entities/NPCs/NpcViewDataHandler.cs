using Factories;
using Network.Object.Dynamic.Transformations;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization.Entities.NPC
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
