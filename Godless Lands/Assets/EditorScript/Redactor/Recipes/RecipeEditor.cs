#if UNITY_EDITOR
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Recipes
{
    public class RecipeEditor : Editor
    {
        private Recipe recipe;
        public ItemsList itemsList;
        public void Select(object selectObject)
        {
            recipe = selectObject as Recipe;
        }
        public System.Object GetSelectObject()
        {
            return recipe;
        }
    }
}
#endif