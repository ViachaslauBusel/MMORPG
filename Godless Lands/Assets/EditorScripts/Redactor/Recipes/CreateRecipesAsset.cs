#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Recipes
{
    public class CreateRecipesAsset
    {
        [MenuItem("Assets/Create/RecipesList")]
        public static void Create()
        {
            CreateAsset.Create<RecipesList>("RecipesList");
        }

    }
}
#endif