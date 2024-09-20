using Factories;
using Helpers;
using NPCs;
using ObjectRegistryEditor;
using OpenWorld.DataStore;
using Protocol.Data.SpawnData;
using Units.Monster;
using Units.Resource.Data;
using UnityEngine;

namespace OpenWorld.SpawnData
{
    [ExecuteInEditMode]
    public class SpawnPointDataSync<T, K> : MapEntityDataSync<T>, ISpawnPoint where T : SpawnPointData, new() where K : ScriptableObject, IDataObject, IPrefabHolder
    {
        [SerializeField]
        private K _dataObject;
        [SerializeField]
        private SpawnPointType _spawnPointType;
        [SerializeField]
        private float _spawnRadius = 1f;
        [SerializeField]
        private float _minSpawnTime = 1f;
        [SerializeField]
        private float _maxSpawnTime = 1f;
        private int _prefabInstanceId;
        private GameObject _prefabInstance;

        public UnitType UnitType
        {
            get
            {
                switch (_dataObject)
                {
                    case MonsterData:
                        return UnitType.Monster;
                    case NPCData:
                        return UnitType.NPC;
                    case ResourceHarvestData:
                        return UnitType.Resource;
                    default:
                        return UnitType.Unknown;
                }
            }
        }
        public int UnitID => _dataObject?.ID ?? 0;
        public SpawnPointType SpawnPointType => _spawnPointType;
        public float SpawnRadius => _spawnRadius;
        // Time after which the spawn will occur
        public float MinSpawnTime => _minSpawnTime;
        // Time during which the spawn will occur
        public float MaxSpawnTime => _maxSpawnTime;

        protected override void LoadDataProperties(T data)
        {
            _dataObject = (K)data.DataObject;
            _spawnPointType = data.SpawnPointType;
            _spawnRadius = data.SpawnPointRadius;
            _minSpawnTime = data.MinSpawnTime;
            _maxSpawnTime = data.MaxSpawnTime;
            UpdateInstance();
        }

        protected override void SaveDataProperties(ref T data)
        {
            data.DataObject = _dataObject;
            data.SpawnPointType = _spawnPointType;
            data.SpawnPointRadius = _spawnRadius;
            data.MinSpawnTime = _minSpawnTime;
            data.MaxSpawnTime = _maxSpawnTime;
        }

        private void UpdateInstance()
        {
            if (ReadDataMode) return;
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
