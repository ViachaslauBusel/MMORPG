using Protocol;
using Protocol.MSG.Game.Professions;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professions
{
    public class ProfessionsListener : IDisposable
    {
        private ProfessionsModel _professionsModel;
        private NetworkManager _networkManager;

        public ProfessionsListener(ProfessionsModel professionsModel, NetworkManager networkManager)
        {
            _professionsModel = professionsModel;
            _networkManager = networkManager;
            _networkManager.RegisterHandler(Opcode.MSG_PROFESSIONS_SYNC, ProfessionsUpdate);
        }

        private void ProfessionsUpdate(Packet packet)
        {
            packet.Read(out MSG_PROFESSIONS_SYNC_SC msg);

            foreach (var profession in msg.Professions)
            {
                _professionsModel.UpdateProfession(profession.ProfessionType, profession.Level, profession.Experience, profession.ExpForLevelUp);
            }
            _professionsModel.NotifyProfessionUpdates();
        }

        public void Dispose()
        {
           _networkManager.UnregisterHandler(Opcode.MSG_PROFESSIONS_SYNC);
        }
    }
}
