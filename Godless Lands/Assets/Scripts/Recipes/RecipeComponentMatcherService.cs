using Items;
using Protocol.Data.Units.CraftingStation;
using Recipes.Data;
using System.Collections.Generic;
using System.Linq;

namespace Recipes
{
    public class RecipeComponentMatcherService
    {
        private RecipesRegistry _recipesRegistry;

        public RecipeComponentMatcherService(RecipesRegistry recipesRegistry)
        {
            _recipesRegistry = recipesRegistry;
        }

        public List<RecipeData> FindRecipe(IReadOnlyCollection<Item> component, IReadOnlyCollection<Item> fuel, CraftingStationType machineUse)
        {
            return _recipesRegistry.Objects.Where(r => Match(component, fuel, machineUse, r)).ToList();
        }

        private bool Match(IReadOnlyCollection<Item> component, IReadOnlyCollection<Item> fuel, CraftingStationType machineUse, RecipeData recipe)
        {
            if (recipe.Components.Count == component.Count(i => i != null) && recipe.Fuels.Count == fuel.Count(i => i != null))
            {
                if (EqualsMachine(recipe.StationType, machineUse) == false) return false;
                return ContainsAllItems(recipe.Components, component) && ContainsAllItems(recipe.Fuels, fuel);
            }
            return false;
        }

        private bool EqualsMachine(CraftingStationType useType, CraftingStationType machineUse)
        {
            return useType == machineUse;
        }

        private bool ContainsAllItems(IReadOnlyCollection<ItemBundleLink> pieces, IReadOnlyCollection<Item> items)
        {
            foreach (ItemBundleLink piece in pieces)
            {
                if (!items.Any(i => i != null && i.Data.ID == piece.ID)) return false;
            }
            return true;
        }

        public RecipeData GetRecipeByResultItemId(int id)
        {
            return _recipesRegistry.Objects.FirstOrDefault(r => r.Result.Item.ID == id);
        }
    }
}
