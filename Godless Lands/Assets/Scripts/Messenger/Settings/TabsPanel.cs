using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Messenger.Settings
{
    public class TabsPanel : MonoBehaviour
    {
        [SerializeField] GameObject tabPrefab;
        [SerializeField] Transform content;
        private RectTransform tabCreator;

        private List<GameObject> buttonsOBJ = new List<GameObject>();

        private void Awake()
        {
            tabCreator = GetComponentInChildren<TabCreator>().transform as RectTransform;
        }
        private void Start()
        {
            MessengerSettings.Instance.update += UpdateTabs;
        }

        private void UpdateTabs()
        {
            foreach (GameObject delOBJ in buttonsOBJ)
            {
                Destroy(delOBJ);
            }
            buttonsOBJ.Clear();
            foreach (Tab tab in MessengerSettings.Instance.EditableTabs)
            {
                GameObject obj = Instantiate(tabPrefab);
                obj.transform.SetParent(content);
                TabField tabButton = obj.GetComponent<TabField>();
                tabButton.Load(tab);
                buttonsOBJ.Add(obj);
            }
            tabCreator.SetAsLastSibling();
        }
    }
}