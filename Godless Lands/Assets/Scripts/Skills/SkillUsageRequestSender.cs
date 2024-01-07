using Protocol.MSG.Game.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Skills
{
    public class SkillUsageRequestSender : MonoBehaviour
    {
        private NetworkManager _networkManager;

        [Inject]
        public void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void SendSkillUsageRequest(int skillID)
        {
            MSG_SKILL_USE_CS request = new MSG_SKILL_USE_CS();
            request.SkillID = skillID;
            _networkManager.Client.Send(request);
        }
    }
}
