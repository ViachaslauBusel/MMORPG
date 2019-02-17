using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemsList : ScriptableObject
    {

        [HideInInspector]
        [SerializeField]
        public List<Item> items;

        public void AddItem(Item _item)
        {
            if (items == null)
            {
                items = new List<Item>();
            }
            if (_item.id == 0) _item.id++;

            while (ConstainsKey(_item.id)) _item.id++;

            items.Add(_item);

        }

        public void RemoveItem(Item _item)
        {
            if (_item == null) return;
            items.Remove(_item);
        }

        private bool ConstainsKey(int id)
        {
            foreach (Item _item in items)
            {
                if (_item.id == id) return true;
            }
            return false;
        }


        public int Count
        {
            get { return items.Count; }
        }

        public Item this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= items.Count) return null;
                return items[index];
            }
        }

        public Item GetItem(int id)
        {
            foreach (Item _item in items)
            {
                if (_item.id == id) return _item;
            }
            return null;
        }

        public List<Item> GetList()
        {
            return items;
        }

        public string[] GetNames()
        {
            string[] names = new string[items.Count];
            for(int i=0; i<names.Length; i++)
            {
                names[i] = items[i].nameItem;
            }
            return names;
        }
    }
}