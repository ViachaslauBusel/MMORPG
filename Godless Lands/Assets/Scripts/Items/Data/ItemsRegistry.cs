using ObjectRegistryEditor;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemsData", menuName = "Registry/Items", order = 1)]
    public class ItemsRegistry : DataObjectRegistry<ItemData>
    {
    }
}