using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Skins;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Skins
{
    internal class CharacterViewDataHandler : MonoBehaviour, ISkinObject, INetworkDataHandler
    {
        private GameObject m_characterObject;
        private CharactersFactory m_charatersFactory;

        public event Action<GameObject> updateSkinObject;

        public GameObject SkinObject => m_characterObject;

        [Inject]
        private void Construct(CharactersFactory charactersFactory) 
        {
            m_charatersFactory = charactersFactory;
        }
        public void UpdateData(IReplicationData updatedData)
        {
            CharacterSkinData skinData = (CharacterSkinData)updatedData;

            if(m_characterObject!= null) { Destroy(m_characterObject); }

            m_characterObject = m_charatersFactory.CreateHumanFemale(transform, GetComponent<TransfromDataHandler>().Position);

            updateSkinObject?.Invoke(m_characterObject);
        }
    }
}
