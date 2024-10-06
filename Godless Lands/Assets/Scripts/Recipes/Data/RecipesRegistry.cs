using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Items.Types;
using Protocol.Data.Recipes;
using System.Collections.Generic;
using UnityEngine;

namespace Recipes.Data
{
    [CreateAssetMenu(fileName = "RecipesRegistry", menuName = "Registry/Recipes", order = 1)]
    public class RecipesRegistry : DataObjectRegistry<RecipeData>
    {
        public override void Export()
        {
            var itemsData = new List<RecipeSData>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var item = Objects[i].ToServerData();
                itemsData.Add(item);
            }
            ExportHelper.WriteToFile("recipes", itemsData);
        }
    }
}
