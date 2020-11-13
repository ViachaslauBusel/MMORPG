using Messenger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Messenger {
    public class TabsGroup : MonoBehaviour
    {
        public static TabsGroup Instance { get; private set; }
        [SerializeField] GameObject tabPrefab;
        private List<TabButton> tabsOBJ = new List<TabButton>();

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            TabsManager.Instance.update += UpdateTabs;
            UpdateTabs();
        }

        private void UpdateTabs()
        {
            foreach (TabButton destoyOBJ in tabsOBJ)
                Destroy(destoyOBJ.gameObject);
            tabsOBJ.Clear();

            foreach (Tab tab in TabsManager.Instance.Tabs)
            {
                GameObject obj = Instantiate(tabPrefab);
                obj.transform.SetParent(transform);
                TabButton tabButton = obj.GetComponent<TabButton>();
                tabButton.Set(tab);
                tabsOBJ.Add(tabButton);
            }

            TabButton button = tabsOBJ.Find((t) => t.Tab.IsSelected);
            if (button != null)
                button.Select();
            else tabsOBJ[0].Select();
        }
        public void UpdateSelect()
        {
            foreach (TabButton tab in tabsOBJ)
                tab.UpdateSelect();
        }
    }
}