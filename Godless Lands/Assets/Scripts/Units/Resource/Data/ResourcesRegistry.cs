using ObjectRegistryEditor;
using UnityEngine;

namespace Units.Resource.Data
{
    [CreateAssetMenu(fileName = "ResourcesRegistry", menuName = "Registry/ResourcesRegistry")]
    public class ResourcesRegistry : DataObjectRegistry<ResourceHarvestData>
    {
    }
}
