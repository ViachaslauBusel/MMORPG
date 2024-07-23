using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Network.Object.Visualization.Entities.Characters
{
    public class AssetHolder
    {
        private AsyncOperationHandle<GameObject> _assetHandle;
        private GameObject _asset;
        private GameObject _objectInstance;

        public AssetHolder(AsyncOperationHandle<GameObject> meshHandle)
        {
            _assetHandle = meshHandle;
            _asset = meshHandle.Result;
        }

        public AssetHolder(GameObject instance)
        {
            _objectInstance = instance;
        }

        public bool IsValid => _assetHandle.IsValid();
        public GameObject InstanceObject => _objectInstance;

        internal GameObject Instantiate(Transform transform)
        {
            if(_objectInstance != null)
            {
                return _objectInstance;
            }
            _objectInstance = GameObject.Instantiate(_asset, transform);
            return _objectInstance;
        }

        internal GameObject Instantiate(Transform transform, Vector3 position, float rotation = 0f)
        {
            _objectInstance = Instantiate(transform);
            _objectInstance.transform.position = position;
            _objectInstance.transform.rotation = Quaternion.Euler(0, rotation, 0);
            return _objectInstance;
        }

        internal void Release()
        {
            if (_objectInstance != null)
            {
                GameObject.Destroy(_objectInstance);
            }
            if (_assetHandle.IsValid())
            {
                Addressables.Release(_assetHandle);
            }
        }

        internal void SetActiveInstance(bool isVisible)
        {
            if (_objectInstance != null)
            {
                _objectInstance.SetActive(isVisible);
            }
        }
    }
}
