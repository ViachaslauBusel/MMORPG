using Inventory;
using Items;
using Protocol.Data.Workbenches;
using Recipes;
using System;
using System.Collections.Generic;

namespace Workbench
{
    public class SmelterModel : IDisposable
    {
        private Item[] _components = new Item[SmelterConfig.COMPONENT_SIZE];
        private Item[] _fuels = new Item[SmelterConfig.FUEL_SIZE];
        private List<Recipe> _recipes = new List<Recipe>();
        private RecipeComponentMatcherService _recipeComponentMatcherService;
        private WorkbenchType _smelterUse;
        private InventoryModel _inventoryModel;

        public IReadOnlyCollection<Item> Components => _components;
        public IReadOnlyCollection<Item> Fuels => _fuels;
        public IReadOnlyCollection<Recipe> Recipes => _recipes;

        public event Action OnContentUpdate;

        public SmelterModel(RecipeComponentMatcherService recipeComponentMatcherService, InventoryModel inventoryModel)
        {
            _recipeComponentMatcherService = recipeComponentMatcherService;
            _inventoryModel = inventoryModel;
            _inventoryModel.OnInventoryUpdate += OnInventoryUpdate;
        }

        private void OnInventoryUpdate()
        {
            UpdateItems(_components);
            UpdateItems(_fuels);
            OnContentUpdate?.Invoke();
        }

        private void UpdateItems(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                    continue;
                items[i] = _inventoryModel.FindItem(items[i].UniqueID);
            }
        }

        public void AddComponent(int index, Item item)
        {
            if (index < 0 || index >= SmelterConfig.COMPONENT_SIZE)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            _components[index] = item;

            _recipes = _recipeComponentMatcherService.FindRecipe(_components, _fuels, _smelterUse);

            OnContentUpdate?.Invoke();
        }

        public void AddFuel(int index, Item item)
        {
            if (index < 0 || index >= SmelterConfig.FUEL_SIZE)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            _fuels[index] = item;

            _recipes = _recipeComponentMatcherService.FindRecipe(Components, Fuels, _smelterUse);

            OnContentUpdate?.Invoke();
        }

        public void ReserverSmelterModel(WorkbenchType workbenchTypeUse)
        {
            _smelterUse = workbenchTypeUse;
            Array.Clear(_components, 0, _components.Length);
            Array.Clear(_fuels, 0, _fuels.Length);
            OnContentUpdate?.Invoke();
        }

        public void Dispose()
        {
            _inventoryModel.OnInventoryUpdate -= OnInventoryUpdate;
        }
    }
}

