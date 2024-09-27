using Network.Object.Visualization;
using Network.Object.Visualization.Entities.Monsters;
using Units.Monster;
using UnityEngine;
using Zenject;

namespace Nickname
{
    public class MonsterNicknameRenderer : NicknameRenderer, IVisualObjectScript
    {
        private MonstersRegistry _monstersRegistry;
        private MonsterViewDataHandler _monsterViewDataHandler;

        [Inject]
        private void Construct(MonstersRegistry monstersRegistry)
        {
            _monstersRegistry = monstersRegistry;
        }

        public void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            _monsterViewDataHandler = networkObjectOwner.GetComponent<MonsterViewDataHandler>();
            if (_monsterViewDataHandler == null)
            {
                Debug.LogError("MonsterViewDataHandler not found");
                return;
            }

            MonsterData monsterData = _monstersRegistry.GetObjectByID(_monsterViewDataHandler.ID);
            if (monsterData == null)
            {
                Debug.LogError("MonsterData not found");
                return;
            }

            SetNickname(monsterData.Name);
        }

        public void DetachFromNetworkObject()
        {
        }
    }
}
