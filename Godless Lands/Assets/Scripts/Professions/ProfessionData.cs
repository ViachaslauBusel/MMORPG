using Protocol.MSG.Game.Professions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professions
{
    public class ProfessionData
    {
        private readonly ProfessionType _profession;
        private int _level;
        private int _exp;
        private int _expForLevelUp;

        public ProfessionType Profession => _profession;
        public int Level => _level;
        public int Exp => _exp;
        public int ExpForLevelUp => _expForLevelUp;

        public ProfessionData(ProfessionType profession, int level, int exp, int expForLevelUp)
        {
            _profession = profession;
            _level = level;
            _exp = exp;
            _expForLevelUp = expForLevelUp;
        }

        public void UpdateProfession(int level, int exp, int expForLevelUp)
        {
            _level = level;
            _exp = exp;
            _expForLevelUp = expForLevelUp;
        }
    }
}
