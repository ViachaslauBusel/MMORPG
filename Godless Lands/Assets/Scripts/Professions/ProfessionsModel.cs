using Protocol.MSG.Game.Professions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professions
{
    public class ProfessionsModel
    {
        private readonly Dictionary<ProfessionType, ProfessionData> _professions = new ();


        public event Action OnProfessionUpdate;

        public IReadOnlyCollection<ProfessionData> Professions => _professions.Values;

        public void UpdateProfession(ProfessionType professionType, int level, int experience, int expForLevelUp)
        {
            GetProfession(professionType).UpdateProfession(level, experience, expForLevelUp);
        }

        private ProfessionData GetProfession(ProfessionType professionType)
        {
            if (_professions.ContainsKey(professionType) == false)
            {
                var newProfession = new ProfessionData(professionType, 0, 0, 0);
                _professions.Add(professionType, newProfession);
            }

            return _professions[professionType];
        }

        public void NotifyProfessionUpdates()
        {
            OnProfessionUpdate?.Invoke();
        }
    }
}
