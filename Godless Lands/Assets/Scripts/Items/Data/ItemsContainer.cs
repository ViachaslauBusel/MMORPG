using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items", order = 1)]
    public class ItemsContainer : ScriptableObject, IEnumerable<ItemData>
    {
        [SerializeField]
        private List<ItemData> items;

        public ItemData Add()
        {
            if (items == null)
                items = new List<ItemData>();

            ItemData _item = new ItemData() { id = 1 };

            while (ConstainsKey(_item.id)) _item.id++;

            items.Add(_item);

            return _item;
        }

        public bool Remove(ItemData _item) => items.Remove(_item);

        private bool ConstainsKey(int id) => items.Any((i) => i.id == id);

        public int Count => items == null? 0 : items.Count; 
        

        public ItemData this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= items.Count) return null;
                return items[index];
            }
        }

        public ItemData GetDuplicateItem(int id) => new ItemData(GetItem(id));
        public ItemData GetItem(int id) => items.Find((i) => i.id == id);

        public IEnumerator<ItemData> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
       
    }
}