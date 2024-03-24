using MonsterRedactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class MonstersFactory : MonoBehaviour
    {
        [SerializeField]
        private MonstersList _monstersList;
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject CreateMonster(int skinID, Transform transform, Vector3 position)
        {
           var monsterData = _monstersList.GetMonster(skinID);

            if(monsterData == null)
            {
                Debug.LogError($"Monster with id {skinID} not found");
                return null;
            }

            return _diContainer.InstantiatePrefab(monsterData.prefab, position, Quaternion.identity, transform);
        }
    }
}
