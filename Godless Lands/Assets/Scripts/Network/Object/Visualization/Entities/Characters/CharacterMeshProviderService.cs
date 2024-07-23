using Cysharp.Threading.Tasks;
using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Network.Object.Visualization.Entities.Characters
{
    public class CharacterMeshProviderService
    {
        private ItemsFactory _itemsFactory;

        public CharacterMeshProviderService(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        internal async UniTask<AssetHolder> GetMeshAsync(int partId)
        {
            var goHandle = Addressables.LoadAssetAsync<GameObject>(_itemsFactory.GetItemData(partId).Prefab);
            await goHandle.Task;

            return new AssetHolder(goHandle);
        }
    }
}
