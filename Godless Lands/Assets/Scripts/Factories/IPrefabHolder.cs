using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Factories
{
    public interface IPrefabHolder
    {
        AssetReferenceT<GameObject> Prefab { get; }
    }
}
