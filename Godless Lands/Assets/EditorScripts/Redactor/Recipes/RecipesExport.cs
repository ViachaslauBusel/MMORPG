#if UNITY_EDITOR
using Assets.EditorScripts;
using Protocol.Data.Recipes;
using Protocol.Data.Workbenches;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Recipes {
    public class RecipesExport
    {
    //    public static void Export(RecipesList recipesList)
    //    {
    //       //List<RecipeData> result = new List<RecipeData>();
    //       // foreach (Recipe recipe in recipesList.recipes)
    //       // {
    //       //     RecipeData recipeData = new RecipeData(
    //       //         recipe.id,
    //       //         (WorkbenchType)recipe.use,
    //       //         recipe.result,
    //       //         (int)recipe.profession,
    //       //         recipe.exp,
    //       //         recipe.stamina,
    //       //         GetComponents(recipe.component),
    //       //         GetComponents(recipe.fuel)
    //       //         );
    //       //     result.Add(recipeData);
    //       // }
    //       // ExportHelper.WriteToFile(@"recipes", result);
    //    }

    //    //private static List<RecipeComponent> GetComponents(List<Piece> pieces)
    //    //{
    //    //    List<RecipeComponent> result = new List<RecipeComponent>();
    //    //    foreach (Piece piece in pieces)
    //    //    {
    //    //        result.Add(new RecipeComponent(piece.ID, piece.count));
    //    //    }
    //    //    return result;
    //    //}
    }
}
#endif