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
        public static SkillsBook Instance { get; private set; }
        public GameObject prefSkillCell;
        public SkillsList skillsList;
        public SkillBranch branchFirst;
        public SkillBranch branchSecond;

        public Text weaponFirstTitle;
        public Text weaponSecondTitle;

        public Transform parentSkillFirst;
        public Transform parentSkillSecond;

        private List<SkillCell> skillFirst;
        private List<SkillCell> skillSecond;

        private Canvas skillsBook;
        private UISort uISort;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            skillsBook = GetComponent<Canvas>();
            skillsBook.enabled = false;
            uISort = GetComponentInParent<UISort>();

            //Заполнить первую ветку умений
            skillFirst = FillBranch(branchFirst, weaponFirstTitle, parentSkillFirst, skillFirst);

            //Заполнить вторую ветку умений, если вторая ветка соответствует первой очистить
            if (branchFirst != branchSecond)
            {
                skillSecond = FillBranch(branchSecond, weaponSecondTitle, parentSkillSecond, skillSecond);
            }
            else
            {
                ClearBranch(weaponSecondTitle, skillSecond);
            }
        }

        public static SkillCell GetSkillCellByID(int id)
        {
            foreach(SkillCell cell in Instance.skillFirst)
            {
                if (cell.GetObjectID() == id)
                    return cell;
            }
            return null;
        }

        public void OpenCloseSkillsbook()
        {

            skillsBook.enabled = !skillsBook.enabled;

            if (skillsBook.enabled) uISort.PickUp(skillsBook);
        }

        private void ClearBranch(Text text, List<SkillCell> list)
        {
            if (list != null) DestroySkill(list);
            text.text = "";
        }
        private List<SkillCell> FillBranch(SkillBranch branch, Text text, Transform parent, List<SkillCell> list)
        {
            if (list != null) DestroySkill(list);
            else list = new List<SkillCell>();

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
                    if (prefSkillCell != null)
                    {
                        GameObject obj = Instantiate(prefSkillCell);
                        obj.transform.SetParent(parent);
                        SkillCell skillCell = obj.GetComponent<SkillCell>();
                        skillCell.PutSkill(skill);
                        list.Add(skillCell);
                    }
                    else Debug.LogError("error");
                }
            }
            return list;
        }


        private void DestroySkill(List<SkillCell> list)
        {
            foreach (SkillCell obj in list) Destroy(obj.gameObject);
            list.Clear();
        }
    }
}