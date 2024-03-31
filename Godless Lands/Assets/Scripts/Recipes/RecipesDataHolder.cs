using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recipes
{
    public class RecipesDataHolder : MonoBehaviour
    {
        [SerializeField]
        private RecipesList _recipesList;

        public List<Recipe> RecipesList => _recipesList.recipes;

        public Recipe GetRecipeByResultItemId(int idItem)
        {
            return _recipesList.GetRecipeByResult(idItem);
        }

        public Recipe GetRecipeById(int id)
        {
            return _recipesList.GetRecipe(id);
        }
    }
}
