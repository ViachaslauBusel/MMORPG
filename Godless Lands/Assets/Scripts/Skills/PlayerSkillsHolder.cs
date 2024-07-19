using Network.Core;
using Protocol;
using Protocol.MSG.Game.Skills;
using RUCP;
using Skills.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Skills
{
    public class PlayerSkillsHolder : MonoBehaviour
    {
        [SerializeField]
        private SkillsRegistry _skillsRegistry;
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

        public SkillData GetSkill(int skillID)
        {
            if(_skills.Contains(skillID) == false) return null;

            SkillData skill = _skillsRegistry.GetObjectByID(skillID);

            return skill;
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_SKILLS_UPDATE);
        }
    }
}
