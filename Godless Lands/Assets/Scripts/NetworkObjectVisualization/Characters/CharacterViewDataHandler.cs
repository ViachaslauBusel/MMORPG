using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitVisualCache;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization
{
    internal class CharacterViewDataHandler : NetworkObjectVisualHandler, INetworkDataHandler
    {
        private CharactersFactory _charatersFactory;
        private CharacterSkinData _visualData;
        private TransfromDataHandler _networkTransform;

        [Inject]
        private void Construct(CharactersFactory charactersFactory)
        {
            _charatersFactory = charactersFactory;
        }

        private new void Awake()
        {
            base.Awake();
            _networkTransform = GetComponent<TransfromDataHandler>();
        }

        public void UpdateData(IReplicationData updatedData)
        {
            _visualData = (CharacterSkinData)updatedData;
            UpdateVisualObject(0);
        }

        protected override GameObject CreateNewUnit()
        {
            return _charatersFactory.CreateHumanMale(transform, _networkTransform.Position);
        }
    }
}
