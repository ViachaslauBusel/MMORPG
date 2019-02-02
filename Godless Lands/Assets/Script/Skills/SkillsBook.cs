using Cells;
using SkillsRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillsBook : MonoBehaviour
    {
        public GameObject prefSkillCell;
        public SkillsList skillsList;
        public SkillBranch branchFirst;
        public SkillBranch branchSecond;

        public Text weaponFirstTitle;
        public Text weaponSecondTitle;

        public Transform parentSkillFirst;
        public Transform parentSkillSecond;

        private List<GameObject> skillFirst;
        private List<GameObject> skillSecond;

        private Canvas skillsBook;
        private UISort uISort;

        private void Start()
        {
            skillsBook = GetComponent<Canvas>();
            skillsBook.enabled = false;
            uISort = GetComponentInParent<UISort>();

            //Заполнить первую ветку умений
            FillBranch(branchFirst, weaponFirstTitle, parentSkillFirst, skillFirst);

            //Заполнить вторую ветку умений, если вторая ветка соответствует первой очистить
            if (branchFirst != branchSecond)
            {
                FillBranch(branchSecond, weaponSecondTitle, parentSkillSecond, skillSecond);
            }
            else
            {
                ClearBranch(weaponSecondTitle, skillSecond);
            }
        }

        public void OpenCloseSkillsbook()
        {

            skillsBook.enabled = !skillsBook.enabled;

            if (skillsBook.enabled) uISort.PickUp(skillsBook);
        }

        private void ClearBranch(Text text, List<GameObject> list)
        {
            if (list != null) DestroySkill(list);
            text.text = "";
        }
        private void FillBranch(SkillBranch branch, Text text, Transform parent, List<GameObject> list)
        {
            if (list != null) DestroySkill(list);
            else list = new List<GameObject>();

            switch (branch)
            {
                case SkillBranch.Sword:
                    text.text = "Мечи";
                    break;
                default:
                    text.text = "";
                    break;
            }

            foreach(Skill skill in skillsList.skills)
            {
                if(skill.branch == branch)
                {
                    GameObject obj = Instantiate(prefSkillCell);
                    obj.transform.SetParent(parent);
                    SkillCell skillCell = obj.GetComponent<SkillCell>();
                    skillCell.PutSkill(skill);
                    list.Add(obj);
                }
            }
        }


        private void DestroySkill(List<GameObject> list)
        {
            foreach (GameObject obj in list) Destroy(obj);
            list.Clear();
        }
    }
}