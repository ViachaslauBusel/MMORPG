using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messenger.Settings
{
    public class TabField : MonoBehaviour
    {
        [SerializeField] Text textName;
        private Tab tab;
        public void Load(Tab tab)
        {
            this.tab = tab;
            textName.text = tab.name;
            GetComponent<Button>().onClick.AddListener(Select);
        }
        public void Select()
        {
            TabRedactor.Instance.Select(tab);
        }
    }
}