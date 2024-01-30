using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using Skins;
using System;
using UnityEngine;
using Zenject;

namespace Walkers.Monsters
{
    internal class MonsterViewDataHandler : MonoBehaviour, ISkinObject, INetworkDataHandler
    {
        private GameObject _unitObject;
        private MonstersFactory _monstersFactory;

        public event Action<GameObject> updateSkinObject;

        public GameObject SkinObject => _unitObject;

        [Inject]
        private void Construct(MonstersFactory monstersFactory)
        {
            _monstersFactory = monstersFactory;
        }
        public void UpdateData(IReplicationData updatedData)
        {
            MonsterSkinData skinData = (MonsterSkinData)updatedData;

            if (_unitObject != null) { Destroy(_unitObject); }

            _unitObject = _monstersFactory.CreateMonster(skinData.SkinID, transform, GetComponent<TransfromDataHandler>().Position);

            updateSkinObject?.Invoke(_unitObject);
        }
    }
}
