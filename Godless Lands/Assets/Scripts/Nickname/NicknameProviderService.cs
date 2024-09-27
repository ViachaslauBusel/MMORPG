using NPCs;
using Protocol.Data.Units;
using Units.Monster;
using Units.Resource.Data;

namespace Nickname
{
    public class NicknameProviderService
    {
        private MonstersRegistry _monstersRegistry;
        private NPCsRegistry _npcsRegistry;
        private ResourcesRegistry _resourcesRegistry;
        //private CraftingStationsRegistry _craftingStationsRegistry;

        public NicknameProviderService(MonstersRegistry monstersRegistry, NPCsRegistry npcsRegistry, ResourcesRegistry resourcesRegistry)
        {
            _monstersRegistry = monstersRegistry;
            _npcsRegistry = npcsRegistry;
            _resourcesRegistry = resourcesRegistry;
            //_craftingStationsRegistry = craftingStationsRegistry;
        }

        public string GetNickname(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            var parts = key.Split(':');
            if (parts.Length < 2 || !int.TryParse(parts[1].Trim(), out int id))
            {
                return key;
            }

            return parts[0] switch
            {
                "m" => _monstersRegistry.GetObjectByID(id)?.Name,
                "n" => _npcsRegistry.GetObjectByID(id)?.Name,
                "r" => _resourcesRegistry.GetObjectByID(id)?.Name,
                //"c" => _craftingStationsRegistry.GetObjectByID(id)?.Name,
                _ => key,
            };
        }

        public string GetNickname(UnitType unitType, int id)
        {
            return unitType switch
            {
                UnitType.Monster => _monstersRegistry.GetObjectByID(id)?.Name,
                UnitType.NPC => _npcsRegistry.GetObjectByID(id)?.Name,
                UnitType.Resource => _resourcesRegistry.GetObjectByID(id)?.Name,
                //UnitType.CraftingStation => _craftingStationsRegistry.GetObjectByID(id)?.Name,
                _ => string.Empty,
            };
        }
    }
}
