using Cysharp.Threading.Tasks;
using Network.Object.Visualization.Entities.Characters;
using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Factories
{
    public class AddressablesAssetFactory : MonoBehaviour
    {
        protected async UniTask<AssetHolder> CreateAssetHolder(IDataObjectRegistry registry, int objectID)
        {
            var dataObject = registry.GetObjectByID(objectID);

            if (dataObject == null)
            {
                Debug.LogError($"Asset with id {objectID} not found");
                return null;
            }

            IPrefabHolder prefabHolder = dataObject as IPrefabHolder;
            if (prefabHolder == null) 
            {
                Debug.LogError($"Asset with id {objectID} is not a prefab holder");
                return null;
            }

            var assetHandler = Addressables.LoadAssetAsync<GameObject>(prefabHolder.Prefab);

            await assetHandler.Task;

            if (assetHandler.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(assetHandler);
                Debug.LogError($"Failed to load asset {prefabHolder.Prefab}");
                return null;
            }

            return new AssetHolder(assetHandler);
        }
    }
}
