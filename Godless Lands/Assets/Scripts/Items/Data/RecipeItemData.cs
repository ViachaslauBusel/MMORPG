using Protocol.Data.Workbenches;
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
        private WorkbenchType _workbenchType;

        public IReadOnlyCollection<ItemBundle> Components => _components;
        public IReadOnlyCollection<ItemBundle> Fuel => _fuel;
        public WorkbenchType WorkbenchType => _workbenchType;
    }
}
