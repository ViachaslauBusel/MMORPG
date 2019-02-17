using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Recipes
{
    public class RecipesList : ObjectList
    {
        public List<Recipe> recipes;

        public override void Add(object obj)
        {
            Recipe recipe = obj as Recipe;
            if (recipe == null) return;
            if (recipes == null) recipes = new List<Recipe>();
            if (recipe.id == 0) recipe.id++;

            while (ConstainsKey(recipe.id)) recipe.id++;
            recipes.Add(recipe);
        }

        private bool ConstainsKey(int id)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipe.id == id) return true;
            }
            return false;
        }
        public override int Count
        {
            get
            {
                if (recipes == null) return 0;
                return recipes.Count;
            }
        }
        public override System.Object this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= recipes.Count) return null;
                return recipes[index];
            }
        }

        public void Remove(Recipe recipe)
        {
            if (recipe == null) return;
            recipes.Remove(recipe);
        }

        public override void Remove(System.Object obj)
        {
           Remove(obj as Recipe);
        }
    }
}