using AssetPerformanceToolkit.AssetManagement;
using Cysharp.Threading.Tasks;
using ObjectRegistryEditor;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class AddressablesAssetFactory
    {
        private DiContainer _diContainer;

        public AddressablesAssetFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        protected async UniTask<AssetInstance> LoasAssetInstanceAssync(IDataObjectRegistry registry, int objectID)
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

            var assetHandler = await AssetLoader.LoadInstance(prefabHolder.Prefab, _diContainer.InstantiatePrefab);

            if (assetHandler.IsValid == false)
            {
                assetHandler.Release();
                Debug.LogError($"Failed to load asset {prefabHolder.Prefab}");
                return null;
            }

            return assetHandler;
        }
    }
}
