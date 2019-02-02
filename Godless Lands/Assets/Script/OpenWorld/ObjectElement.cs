using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    //сохроняет обьекты на тайле 100 на 100 в лист MapObject
    public class ObjectElement : ScriptableObject
    {
       public List<MapObject> mapObjects;

        public void Add(MapObject obj)
        {
            if (mapObjects == null) mapObjects = new List<MapObject>();
            mapObjects.Add(obj);
        }
    }
}