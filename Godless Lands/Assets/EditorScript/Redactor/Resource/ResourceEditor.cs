﻿#if UNITY_EDITOR
using Items;
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Resource
{
    public class ResourceEditor : Editor
    {
        private Fabric resource;
        public ItemsList itemsList;
        public  void Select(object selectObject, ItemsList itemsList)
        {
            resource = selectObject as Fabric;
            this.itemsList = itemsList;
        }
        public System.Object GetSelectObject()
        {
            return resource;
        }
    }
}
#endif