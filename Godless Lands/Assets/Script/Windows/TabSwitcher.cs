using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class TabSwitcher : MonoBehaviour
    {
        [SerializeField] List<Tab> tabs;

        private void Start()
        {
            foreach(Tab tab in tabs)
            {
                tab.button.onClick.AddListener(() =>
                {
                    HideAll();
                    foreach (GameObject obj in tab.elements)
                        obj.SetActive(true);
                });
            }
        }

        private void HideAll()
        {
            foreach(Tab tab in tabs)
            {
                tab.button.interactable = true;
                foreach (GameObject obj in tab.elements)
                    obj.SetActive(false);
            }
        }
    }
    [System.Serializable]
    public class Tab
    {
        public Button button;
        public List<GameObject> elements = new List<GameObject>();
    }
}