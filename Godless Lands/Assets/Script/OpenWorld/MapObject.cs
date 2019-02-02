using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    //Данные для сохранение одного элемента карты
    [System.Serializable]
    public class MapObject
    {
        public Vector3 postion;
        public Quaternion orientation;
        public Vector3 scale;
        public GameObject prefab;

        public void Set(GameObject objPrefab, GameObject obj)
        {
            postion = obj.transform.position;
            orientation = obj.transform.rotation;
            scale = obj.transform.localScale;
            prefab = objPrefab;
        }
    }
}