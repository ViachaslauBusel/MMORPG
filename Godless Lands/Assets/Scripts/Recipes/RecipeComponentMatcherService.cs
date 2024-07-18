using Items;
using Items.Data;
using Protocol.Data.Workbenches;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes
{
    public class RecipeComponentMatcherService
    {
        private RecipesDataHolder _recipesDataHolder;

        public RecipeComponentMatcherService(RecipesDataHolder recipesDataHolder)
        {
            _recipesDataHolder = recipesDataHolder;
        }

        public List<RecipeItemData> FindRecipe(IReadOnlyCollection<Item> component, IReadOnlyCollection<Item> fuel, WorkbenchType machineUse)
        {
            return _recipesDataHolder.Recipes.Where(r => Match(component, fuel, machineUse, r)).ToList();
        }

        private bool Match(IReadOnlyCollection<Item> component, IReadOnlyCollection<Item> fuel, WorkbenchType machineUse, RecipeItemData recipe)
        {
            if (recipe.Components.Count == component.Count(i => i != null) && recipe.Fuel.Count == fuel.Count(i => i != null))
            {
                if (EqualsMachine(recipe.WorkbenchType, machineUse) == false) return false;
                return ContainsAllItems(recipe.Components, component) && ContainsAllItems(recipe.Fuel, fuel);
            }
            return false;
        }

        private bool EqualsMachine(WorkbenchType useType, WorkbenchType machineUse)
        {
            return useType == machineUse;
        }

        private bool ContainsAllItems(IReadOnlyCollection<ItemBundle> pieces, IReadOnlyCollection<Item> items)
        {
            foreach (ItemBundle piece in pieces)
            {
                if (!items.Any(i => i != null && i.Data.ID == piece.ID)) return false;
            }
            return true;
        }

        public RecipeItemData GetRecipeByResultItemId(int id)
        {
            return _recipesDataHolder.Recipes.FirstOrDefault(r => r.Result.ID == id);
        }
    }
}
