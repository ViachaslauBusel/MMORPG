using Protocol.MSG.Game.Professions;
using RUCP;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Professions.UI
{
    public class ProfessionBlockView : MonoBehaviour
    {
        [SerializeField]
        private Text _professionNameTxt;        
        [SerializeField]
        private Text _professionLevelTxt;
        [SerializeField]
        private Image _professionExpBar;
        [SerializeField]
        private Text _professionExpTxt;

        internal void UpdateProfession(int level, int exp, int expForLevelUp)
        {
            _professionLevelTxt.text = level.ToString();
            _professionExpBar.fillAmount = exp / (float)expForLevelUp;
            _professionExpTxt.text = $"{exp}/{expForLevelUp}";
        }

        internal void InitProfession(ProfessionType profession, int level, int exp, int expForLevelUp)
        {
            _professionNameTxt.text = GetProfessionName(profession);
            UpdateProfession(level, exp, expForLevelUp);
        }

        private string GetProfessionName(ProfessionType profession) => profession switch
        {
            ProfessionType.Mining => "Mining",
            ProfessionType.Blacksmithing => "Blacksmithing",
            _ => "unknown"
        };
    }
}