using Cysharp.Threading.Tasks;
using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Network.Object.Visualization.Entities.Characters
{
    public class CharacterMeshProviderService
    {
        private DiContainer _diContainer;
        private ItemsFactory _itemsFactory;

        public CharacterMeshProviderService(ItemsFactory itemsFactory, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _itemsFactory = itemsFactory;
        }

        internal async UniTask<AssetHolder> GetMeshAsync(int partId)
        {
            var prefab = _itemsFactory.GetItemData(partId)?.Prefab;

            if (prefab == null)
            {
                return null;
            }

            var goHandle = Addressables.LoadAssetAsync<GameObject>(prefab);

            await goHandle.Task;

            return new AssetHolder(_diContainer, goHandle);
        }
    }
}
