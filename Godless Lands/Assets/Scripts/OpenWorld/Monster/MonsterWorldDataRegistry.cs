using AYellowpaper.SerializedCollections;
using System;
using Units.Monster;
using UnityEditor;
using UnityEngine;

namespace OpenWorld.Monster
{
    [Serializable]
    public class MonsterWorldData
    {
        public MonsterData MonsterData;
        public SpawnPointType SpawnPointType;
        public float SpawnPointRadius;
    }

    [CreateAssetMenu(fileName = "MonsterWorldDataRegistry", menuName = "OpenWorld/MonsterWorldDataRegistry")]
    public class MonsterWorldDataRegistry : ScriptableObject
    {
        [SerializedDictionary("EntityID, MonsterWorldData")]
        public SerializedDictionary<int, MonsterWorldData> _monsterData = new();

        public MonsterWorldData GetMonsterData(int entityID)
        {
            if (_monsterData.TryGetValue(entityID, out MonsterWorldData monsterData))
            {
                return monsterData;
            }

            return null;
        }

        public void AddOrUpdateMonsterData(int entityID, MonsterWorldData monsterData)
        {
            _monsterData[entityID] = monsterData;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }

        internal void RemoveMonsterData(int entityID)
        {
            if (_monsterData.ContainsKey(entityID))
            {
                _monsterData.Remove(entityID);
            }
        }
    }
}
