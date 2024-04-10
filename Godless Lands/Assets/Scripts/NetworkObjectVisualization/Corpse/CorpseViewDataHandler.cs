using Factories;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization.Corpse
{
    internal class CorpseViewDataHandler : CachedNetworkObjectVisualHandler, INetworkDataHandler
    {
        private MonstersFactory _monstersFactory;
        private CorpseSkinData _visualData;
        private TransfromDataHandler _networkTransform;

        protected override bool IsNeedChaceVisual => false;

        [Inject]
        private void Construct(MonstersFactory monstersFactory)
        {
            _monstersFactory = monstersFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();

        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (CorpseSkinData)updatedData;

            UpdateVisualObject();
        }

        protected void UpdateVisualObject()
        {
            DestroyExistingUnitObject();

            GameObject visualObject = GetCachedVisualObject(_visualData.CachedObjectId);
            visualObject ??= CreateNewUnit();

            SetVisualObject(visualObject);
        }

        private GameObject CreateNewUnit()
        {
            return _monstersFactory.CreateMonster(_visualData.SkinID, transform, _networkTransform.Position);
        }
    }
}
