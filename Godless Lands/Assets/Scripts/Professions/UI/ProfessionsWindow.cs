using Protocol.MSG.Game.Professions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Professions.UI
{
    public class ProfessionsWindow : MonoBehaviour
    {
        [SerializeField]
        private ProfessionBlockView _professionBlockViewPrefab;
        private Canvas _canvas;
        private ProfessionsModel _professionsModel;
        private DiContainer _diContainer;
        private Dictionary<ProfessionType, ProfessionBlockView> _professionBlockViews = new ();

        [Inject]
        private void Construct(ProfessionsModel professionsModel, DiContainer diContainer)
        {
            _professionsModel = professionsModel;
            _diContainer = diContainer;

            _professionsModel.OnProfessionUpdate += UpdateProfession;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        private void UpdateProfession()
        {
            //Remove professions that are not in the model
            foreach (var profession in _professionBlockViews.Keys)
            {
                if (_professionsModel.Professions.Any(p => p.Profession == profession) == false)
                {
                    Destroy(_professionBlockViews[profession].gameObject);
                    _professionBlockViews.Remove(profession);
                }
            }

            foreach (var profession in _professionsModel.Professions)
            {
                if(_professionBlockViews.ContainsKey(profession.Profession))
                {
                    _professionBlockViews[profession.Profession].UpdateProfession(profession.Level, profession.Exp, profession.ExpForLevelUp);
                }
                else
                {
                    var professionBlockViewObj = _diContainer.InstantiatePrefab(_professionBlockViewPrefab, _professionBlockViewPrefab.transform.parent);
                    professionBlockViewObj.SetActive(true);
                    ProfessionBlockView professionBlock = professionBlockViewObj.GetComponent<ProfessionBlockView>();
                    professionBlock.InitProfession(profession.Profession, profession.Level, profession.Exp, profession.ExpForLevelUp);
                    _professionBlockViews.Add(profession.Profession, professionBlock);
                }
            }
        }

        public void OpenClose()
        {
            _canvas.enabled = !_canvas.enabled;
        }
    }
}