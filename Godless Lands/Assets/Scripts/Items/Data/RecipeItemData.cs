using Protocol.Data.Units.CraftingStation;
using Recipes.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Data
{
    public class RecipeItemData : ItemData
    {

        [SerializeField]
        private RecipeData _recipe;

        public RecipeData Recipe => _recipe;
    }
}
