﻿using System;
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


        public int id;
        public string nameItem;
        public Texture2D texture;
        public string description;
        public bool stack;
        public GameObject prefab;
        public ItemUse use;
        [SerializeField]
        private string _serializableObj;
        [SerializeField]
        private string type;

        public bool IsEmpty()
        {
            if (id < 1) return true;
            return false;
        }
        public System.Object serializableObj
        {
            get
            {
                if (_serializableObj == null || _serializableObj.Length == 0 || string.IsNullOrEmpty(type)) return null;
               
                return JsonUtility.FromJson(_serializableObj, Type.GetType(type));
            }
            set
            {
                if (value == null) { _serializableObj = null; return; }

                _serializableObj = JsonUtility.ToJson(value);
                type = value.GetType().ToString();
            }
        }

        public static ItemUse GetUse(System.Object Obj)
        {
             if (Obj == null) return ItemUse.None;
            Type type = Obj.GetType();
            if (type == typeof(WeaponItem)) return ItemUse.Weapon;
            if (type == typeof(WeaponItem)) return ItemUse.Armor;
            if (type == typeof(RestorePointsItem)) return ItemUse.RestorePoints;
            return ItemUse.None;
        }
    }

    
}