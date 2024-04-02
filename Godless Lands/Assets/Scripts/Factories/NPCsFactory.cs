using MonsterRedactor;
using NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Factories
{
    internal class NPCsFactory : MonoBehaviour
    {
        [SerializeField]
        private NPCList _npcList;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateNPC(int skinID, Transform transform, Vector3 position, float rotation)
        {
            var npcData = _npcList.GetNPC(skinID);

            if (npcData == null)
            {
                Debug.LogError($"Monster with id {skinID} not found");
                return null;
            }

            return _diContainer.InstantiatePrefab(npcData.prefab, position, Quaternion.Euler(0, rotation, 0), transform);
        }
    }
}
