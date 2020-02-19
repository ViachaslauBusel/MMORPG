using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redactor
{
    public class ObjectList : ScriptableObject
    {
        public virtual void Add(System.Object obj)
        {

        }
        public virtual int Count
        {
            get { return 0; }
        }

        public virtual System.Object this[int index]
        {
            get
            {
                return null;
            }
        }

        public virtual void Remove(System.Object obj)
        {

        }

        public virtual void RemoveAt(int index)
        {

        }
    }
}