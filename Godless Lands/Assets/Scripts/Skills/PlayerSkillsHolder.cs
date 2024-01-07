using Cells;
using Protocol;
using Protocol.MSG.Game.Skills;
using RUCP;
using SkillsRedactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Skills
{
    public class PlayerSkillsHolder : MonoBehaviour
    {
        [SerializeField]
        private SkillsList _skillsList;
        private NetworkManager _networkManager;
        private List<int> _skills = new List<int>();

        public IEnumerable<int> Skills => _skills;

        public event Action OnSkillsUpdate;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
            _networkManager.RegisterHandler(Opcode.MSG_SKILLS_UPDATE, SkillsUpdate);
        }

        private void SkillsUpdate(Packet packet)
        {
            packet.Read(out MSG_SKILLS_UPDATE msg);
            _skills = msg.Skills;
            OnSkillsUpdate?.Invoke();
        }

        public Skill GetSkill(int skillID)
        {
            if(_skills.Contains(skillID) == false) return null;

            Skill skill = _skillsList.GetSkill(skillID);

            return skill;
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_SKILLS_UPDATE);
        }

        
    }
}
