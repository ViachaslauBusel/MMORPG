using Items;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Network.Object.Visualization.Entities.Characters
{
    public class ItemPrefabProviderService
    {
        private DiContainer _diContainer;
        private ItemsFactory _itemsFactory;

        public ItemPrefabProviderService(ItemsFactory itemsFactory, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _itemsFactory = itemsFactory;
        }

        public AssetReference GetPrefab(int itemId)
        {
            return _itemsFactory.GetItemData(itemId)?.Prefab;
        }
    }
}
