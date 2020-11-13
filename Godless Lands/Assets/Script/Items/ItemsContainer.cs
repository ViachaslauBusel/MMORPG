using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items", order = 1)]
    public class ItemsContainer : ScriptableObject, IEnumerable<Item>
    {
        [SerializeField]
        private List<Item> items;

        public Item Add()
        {
            if (items == null)
                items = new List<Item>();

            Item _item = new Item() { id = 1 };

            while (ConstainsKey(_item.id)) _item.id++;

            items.Add(_item);

            return _item;
        }

        public bool Remove(Item _item) => items.Remove(_item);

        private bool ConstainsKey(int id) => items.Any((i) => i.id == id);

        public int Count => items == null? 0 : items.Count; 
        

        public Item this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= items.Count) return null;
                return items[index];
            }
        }

        public Item GetDuplicateItem(int id) => new Item(GetItem(id));
        public Item GetItem(int id) => items.Find((i) => i.id == id);

        public IEnumerator<Item> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
       
    }
}