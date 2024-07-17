using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace NetworkObjectVisualization.Characters
{
    public class MeshHolder
    {
        private AsyncOperationHandle<GameObject> _meshHandle;
        private GameObject _meshAsset;
        private GameObject _meshInstance;

        public MeshHolder(AsyncOperationHandle<GameObject> meshHandle)
        {
            _meshHandle = meshHandle;
            _meshAsset = meshHandle.Result;
        }

        public bool IsValid => _meshHandle.IsValid();

        internal GameObject Instantiate(Transform transform)
        {
            if(_meshInstance != null)
            {
                return _meshInstance;
            }
            _meshInstance = GameObject.Instantiate(_meshAsset, transform);
            return _meshInstance;
        }

        internal void Release()
        {
            if (_meshInstance != null)
            {
                GameObject.Destroy(_meshInstance);
            }
            if (_meshHandle.IsValid())
            {
                Addressables.Release(_meshHandle);
            }
        }

        internal void SetActiveInstance(bool isVisible)
        {
            if (_meshInstance != null)
            {
                _meshInstance.SetActive(isVisible);
            }
        }
    }
}
