using OpenWorld.DataStore;
using OpenWorld.SpawnData.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenWorld.SpawnData.Resources
{
    [CreateAssetMenu(fileName = "ResourceWorldDataRegistry", menuName = "OpenWorld/ResourceWorldDataRegistry")]
    public class ResourceWorldDataRegistry : EntityDataStore<ResourceWorldData>
    {
    }
}
