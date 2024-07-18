using Items;
using Items.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes
{
    public class RecipesDataHolder
    {
        private List<RecipeItemData> _recipes;

        public RecipesDataHolder(ItemsRegistry itemsRegistry)
        {
            _recipes = itemsRegistry.Objects.Where(i => i is RecipeItemData).Cast<RecipeItemData>().ToList();
        }

        public IReadOnlyCollection<RecipeItemData> Recipes => _recipes;

        public RecipeItemData GetRecipe(int id) => _recipes.FirstOrDefault(r => r.ID == id);
    }
}
