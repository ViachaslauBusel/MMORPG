using ObjectRegistryEditor;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemsRegistry", menuName = "Registry/Items", order = 1)]
    public class ItemsRegistry : DataObjectRegistry<ItemData>
    {
    }
}