using Data;
using Helpers;
using ObjectRegistryEditor;
using OpenWorld.DataStore;
using UnityEngine;

namespace OpenWorld.SpawnData
{
    [ExecuteInEditMode]
    public class SpawnPointDataSync<T, K> : MapEntityDataSync<T> where T : SpawnPointData, new() where K : ScriptableObject, IDataObject, IPrefabHolder
    {
        [SerializeField]
        private K _dataObject;
        [SerializeField]
        private SpawnPointType _spawnPointType;
        [SerializeField]
        private float _spawnRadius = 1f;
        private int _prefabInstanceId;
        private GameObject _prefabInstance;

        protected override void LoadDataProperties(T data)
        {
            _dataObject = (K)data.DataObject;
            _spawnPointType = data.SpawnPointType;
            _spawnRadius = data.SpawnPointRadius;

            UpdateInstance();
        }

        protected override void SaveDataProperties(ref T data)
        {
            data.DataObject = _dataObject;
            data.SpawnPointType = _spawnPointType;
            data.SpawnPointRadius = _spawnRadius;
        }

        private void UpdateInstance()
        {
            if (_prefabInstance != null)
            {
                _prefabInstanceId = 0;
                DestroyImmediate(_prefabInstance);
                _prefabInstance = null;

            }
            if (_dataObject != null && _dataObject.Prefab != null)
            {
                _prefabInstanceId = _dataObject.ID;
                _prefabInstance = Instantiate(_dataObject.Prefab.editorAsset);
                _prefabInstance.transform.SetParent(transform);
                _prefabInstance.transform.localPosition = Vector3.zero;
                _prefabInstance.transform.localRotation = Quaternion.identity;
            }
        }

        private void Update()
        {
            if (_prefabInstanceId != (_dataObject?.ID ?? 0))
            {
                UpdateInstance();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            switch (_spawnPointType)
            {
                case SpawnPointType.Circle:
                    DrawGizmoHelper.DrawGizmoCircleStack(transform.position, _spawnRadius, 3);
                    break;
                case SpawnPointType.Square:
                    DrawGizmoHelper.DrawGizmoSquareStack(transform.position, _spawnRadius, 3);
                    break;
            }
        }
    }
}
