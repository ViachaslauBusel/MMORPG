using Items;
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


        public List<Recipe> FindRecipe(IReadOnlyCollection<Item> component, IReadOnlyCollection<Item> fuel, WorkbenchType machineUse)
        {
            List<Recipe> result = new List<Recipe>();
            foreach (Recipe recipe in _recipesDataHolder.RecipesList)
            {
                if (Match(component, fuel, machineUse, recipe))
                {
                    result.Add(recipe);
                }
            }
            return result;
        }

        private bool Match(IReadOnlyCollection<Item> component, IReadOnlyCollection<Item> fuel, WorkbenchType machineUse, Recipe recipe)
        {
            if (recipe.component.Count == component.Count(i => i != null) && recipe.fuel.Count == fuel.Count(i => i != null))
            {
                if (EqualsMachine(recipe.use, machineUse) == false) return false;
                return ContainsAllItems(recipe.component, component) && ContainsAllItems(recipe.fuel, fuel);
            }
            return false;
        }

        private bool EqualsMachine(MachineUse use, WorkbenchType machineUse)
        {
            WorkbenchType useType = use switch
            {
                MachineUse.Smelter => WorkbenchType.Smelter,
                MachineUse.Workbench => WorkbenchType.Workbench,
                MachineUse.Tannery => WorkbenchType.Tannery,
                MachineUse.Grindstone => WorkbenchType.Grindstone,

                _ => WorkbenchType.None,
            };
            return useType == machineUse;
        }

        private bool ContainsAllItems(List<Piece> pieces, IReadOnlyCollection<Item> items)
        {
            foreach (Piece piece in pieces)
            {
                if (!items.Any(i => i != null && i.Data.id == piece.ID)) return false;
            }
            return true;
        }
    }
}
