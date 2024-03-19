using DataFileProtocol.Skills;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using System;
using UnitVisualCache;
using UnityEngine;
using Walkers.Monsters;
using Zenject;

namespace NetworkObjectVisualization
{
    internal class MonsterViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private MonstersFactory _monstersFactory;
        private MonsterSkinData _visualData;
        private TransfromDataHandler _networkTransform;


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
            _visualData = (MonsterSkinData)updatedData;

            Debug.Log("MonsterViewDataHandler: UpdateData: " + _visualData.InNeedChaceVisual);

            UpdateVisualObject(_visualData.VisualChaneObjectId);
        }

        protected void UpdateVisualObject(int visualObjectId)
        {
            DestroyExistingUnitObject();

            GameObject visualObject = GetCachedVisualObject(visualObjectId) ?? CreateNewUnit();

            SetVisualObject(visualObject);
        }

        private GameObject CreateNewUnit()
        {
            return _monstersFactory.CreateMonster(_visualData.SkinID, transform, _networkTransform.Position);
        }
    }
}
