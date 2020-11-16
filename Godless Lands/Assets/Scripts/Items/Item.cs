using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class Item
    {
        [NonSerialized]
        public int objectID = 0;
        [NonSerialized]
        public int count = 0;
        [NonSerialized]
        public int enchant_level;
        [NonSerialized]
        public int durability;
        [NonSerialized]
        public int maxDurability;

        public int id;
        public string nameItem;
        public Texture2D texture;
        public string description;
        public bool stack;
        public int weight;
        public GameObject prefab;
        public ItemType type;
        [SerializeField]
        private string _serializableObj;
        [SerializeField]
        private string typeObject;

        public Item(Item item)
        {
            if(item == null) { id = 0; return; }
            id = item.id;
            nameItem = item.nameItem;
            texture = item.texture;
            description = item.description;
            stack = item.stack;
            weight = item.weight;
            prefab = item.prefab;
            type = item.type;
            _serializableObj = item._serializableObj;
            typeObject = item.typeObject;
        }
        public Item()
        {
            id = 0;
        }

        public bool IsExist()
        {
            if (id > 0) return true;
            return false;
        }
        public System.Object serializableObj
        {
            get
            {
                if (_serializableObj == null || _serializableObj.Length == 0 || string.IsNullOrEmpty(typeObject)) return null;
               
                return JsonUtility.FromJson(_serializableObj, Type.GetType(typeObject));
            }
            set
            {
                if (value == null) { _serializableObj = null; return; }

                _serializableObj = JsonUtility.ToJson(value);
                typeObject = value.GetType().ToString();
            }
        }

        public static ItemType GetUse(System.Object Obj)
        {
             if (Obj == null) return ItemType.None;
            Type type = Obj.GetType();
            if (type == typeof(WeaponItem)) return ItemType.Weapon;
            if (type == typeof(WeaponItem)) return ItemType.Armor;
            if (type == typeof(RestorePointsItem)) return ItemType.RestorePoints;
            if (type == typeof(RecipesItem)) return ItemType.Recipes;
            return ItemType.None;
        }
    }

    
}