using Items;
using ObjectRegistryEditor;
using Protocol.Data.Recipes;
using Protocol.Data.Units.CraftingStation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Recipes.Data
{
    public class RecipeData : ScriptableObject, IDataObject
    {
        [SerializeField, HideInInspector]
        private int _id;
        [SerializeField]
        private CraftingStationType _stationType;
        [SerializeField]
        private List<ItemBundleLink> _components;
        [SerializeField]
        private List<ItemBundleLink> _fuels;
        [SerializeField]
        private ItemBundleData _result;

        public int ID => _id;
        public string Name => _result?.Item?.Name;
        public CraftingStationType StationType => _stationType;
        public Texture Preview => _result?.Item?.Preview;
        public IReadOnlyCollection<ItemBundleLink> Components => _components;
        public IReadOnlyCollection<ItemBundleLink> Fuels => _fuels;
        public ItemBundleData Result => _result;

        public void Initialize(int id)
        {
            _id = id;
        }

        internal RecipeSData ToServerData()
        {
            return new RecipeSData(_id, _stationType, _result.Item.ID, 0, 0, 0, _components.Select(c => c.ToServerData()).ToList(), _fuels.Select(f => f.ToServerData()).ToList());
        }
    }
}
