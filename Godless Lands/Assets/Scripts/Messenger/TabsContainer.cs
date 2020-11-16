using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Messenger
{
    [System.Serializable]
    public class TabsContainer
    {
        [SerializeField] List<Tab> tabs = new List<Tab>();
        [SerializeField] int uniqueID = 0;

        internal void Add(Tab tab)
        {
            tab.ID = uniqueID++;
            tabs.Add(tab);
        }

        public IEnumerator<Tab> GetEnumerator() => tabs.GetEnumerator();

        public Tab this[int index] => tabs[index];
        public int Count => tabs.Count;

        internal void Remove(Tab tab)
        {
            if (tabs.Count == 1) return;
            tabs.Remove(tab);
        }

        internal bool Contains(Tab selectTab) => tabs.Any<Tab>((t) => t.Equals(selectTab));
        public Tab Find(int id) => tabs.Find((t) => t.ID == id);

    }
}