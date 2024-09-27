using Protocol.Data.Units.CraftingStation;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Data
{
    public class RecipeItemData : ItemData
    {
        [SerializeField]
        private List<ItemBundle> _components;
        [SerializeField]
        private List<ItemBundle> _fuel;
        [SerializeField]
        private CraftingStationType _stationType;
        [SerializeField]
        private ItemBundle _result;

        public IReadOnlyCollection<ItemBundle> Components => _components;
        public IReadOnlyCollection<ItemBundle> Fuel => _fuel;
        public CraftingStationType StationType => _stationType;
        public ItemBundle Result => _result;
    }
}
