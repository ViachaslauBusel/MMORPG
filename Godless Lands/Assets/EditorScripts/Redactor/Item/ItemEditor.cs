
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ItemsRedactor
{
    public class ItemEditor : ScriptableObject
    {

        public Item _item;
        public System.Object serializableObject;

       public void Select(Item item)
        {
            _item = item;
            serializableObject = item.serializableObj;
        }
    }
}
