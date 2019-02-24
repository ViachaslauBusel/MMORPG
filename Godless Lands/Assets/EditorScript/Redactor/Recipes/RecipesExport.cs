#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Recipes {
    public class RecipesExport
    {
        public static void Export(RecipesList recipesList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/recipes.dat", FileMode.Create)))
            {
                foreach (Recipe recipe in recipesList.recipes)
                {
                    stream_out.Write(recipe.id);
                    stream_out.Write((int)recipe.use);
                    stream_out.Write(recipe.result);

                    stream_out.Write((int)recipe.profession);
                    stream_out.Write(recipe.exp);
                    stream_out.Write(recipe.stamina);

                    stream_out.Write((byte)recipe.component.Count);
                    foreach (Piece component in recipe.component)
                    {
                        stream_out.Write(component.ID);
                        stream_out.Write(component.count);//Count
                    }

                    stream_out.Write((byte)recipe.fuel.Count);
                    foreach (Piece fuel in recipe.fuel)
                    {
                        stream_out.Write(fuel.ID);
                        stream_out.Write(fuel.count);//Count
                    }
                }
            }
        }
    }
}
#endif