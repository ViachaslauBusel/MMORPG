#if UNITY_EDITOR
using ObjectRegistryEditor;
using OpenWorld.Monster;
using System.Linq;
using Units.Monster;
using UnityEditor;
using UnityEngine;

namespace OpenWorld.Monster
{
    [ExecuteInEditMode]
    public class MonsterSpawnPointData : MapEntityIdentifier
    {
        [SerializeField]
        private MonsterWorldDataRegistry _monsterWorldDataRegistry;
        [SerializeField]
        private MonsterData _monster;
        [SerializeField]
        private SpawnPointType _spawnPointType;
        [SerializeField]
        private float _spawnRadius = 1f;
        private int _monsterInstanceId;
        private GameObject _monsterInstance;


        protected override void OnIdentifierInitialize()
        {
            Debug.Log($"MonsterSpawnPointData OnIdentifierInitialize{ID}");
            var monsterData = _monsterWorldDataRegistry.GetMonsterData(ID);
            _monster = monsterData?.MonsterData;
            _spawnPointType = monsterData?.SpawnPointType ?? SpawnPointType.Circle;
            _spawnRadius = monsterData?.SpawnPointRadius ?? 1f;

            UpdateInstance();
        }

        private void UpdateInstance()
        {
            if(_monsterInstance != null)
            {
                _monsterInstanceId = 0;
                DestroyImmediate(_monsterInstance);
                _monsterInstance = null;

            }
            if (_monster != null && _monster.Prefab != null)
            {
                _monsterInstanceId = _monster.ID;
                _monsterInstance = Instantiate(_monster.Prefab.editorAsset);
                _monsterInstance.transform.SetParent(transform);
                _monsterInstance.transform.localPosition = Vector3.zero;
                _monsterInstance.transform.localRotation = Quaternion.identity;
            }
        }

        private void Update()
        {
            if (_monsterInstanceId != (_monster?.ID ?? 0))
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
                    DrawGizmoCircle(transform.position + Vector3.up * 0.5f, _spawnRadius);
                    break;
                case SpawnPointType.Square:
                    Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, new Vector3(_spawnRadius, 0, _spawnRadius));
                    break;
            }
        }

        void DrawGizmoCircle(Vector3 center, float radius)
        {
            int segments = 36;
            float angle = 0f;
            Vector3 prevPos = Vector3.zero;

            for (int i = 0; i <= segments; i++)
            {
                angle = i * Mathf.PI * 2f / segments;
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) + center;

                if (i > 0)
                {
                    Gizmos.DrawLine(newPos, prevPos);
                }

                prevPos = newPos;
            }
        }

        protected override void OnIdentifierDestroy()
        {
            _monsterWorldDataRegistry.RemoveMonsterData(ID);
        }

        private void OnDestroy()
        {
            if (ID == 0) return;

            _monsterWorldDataRegistry.AddOrUpdateMonsterData(ID, new MonsterWorldData { MonsterData = _monster, SpawnPointType = _spawnPointType, SpawnPointRadius = _spawnRadius });
        }
    }
}
#endif