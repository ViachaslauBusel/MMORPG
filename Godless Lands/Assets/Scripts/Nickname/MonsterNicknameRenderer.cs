using Network.Object.Visualization.Entities.Monsters;
using Units.Monster;
using UnityEngine;
using Zenject;

namespace Nickname
{
    public class MonsterNicknameRenderer : NicknameRenderer
    {
        private MonstersRegistry _monstersRegistry;
        private MonsterViewDataHandler _monsterViewDataHandler;

        [Inject]
        private void Construct(MonstersRegistry monstersRegistry)
        {
            _monstersRegistry = monstersRegistry;
        }

        private void Awake()
        {
            _monsterViewDataHandler = GetComponentInParent<MonsterViewDataHandler>();
            if (_monsterViewDataHandler == null)
            {
                Debug.LogError("MonsterViewDataHandler not found");
                return;
            }

            MonsterData monsterData = _monstersRegistry.GetObjectByID(_monsterViewDataHandler.ID);
            if(monsterData == null)
            {
                Debug.LogError("MonsterData not found");
                return;
            }

            SetNickname(monsterData.Name);
        }
    }
}
