using Protocol.Data.SpawnData;

namespace OpenWorld.SpawnData
{
    internal interface ISpawnPoint
    {
        UnitType UnitType { get; }
        int UnitID { get; }
        SpawnPointType SpawnPointType { get; }
        float SpawnRadius { get; }
        float MinSpawnTime { get; }
        float MaxSpawnTime { get; }

        void LoadData(int id, bool readDataMode = false);
    }
}