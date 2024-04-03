using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectRegistryEditor
{
    public interface IEditableObject
    {
        int ID { get; }
        string Name { get; }
        Texture Preview { get; }


        void Initialize(int iD);
    }
}
