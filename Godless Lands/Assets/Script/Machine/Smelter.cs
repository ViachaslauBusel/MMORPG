using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipes;
using RUCP;

public class Smelter : MonoBehaviour, Machine
{
    private Canvas canvas;
    public Components component;
    public Components fuel;
    public RecipesList recipesList;
    public Transform recipesParent;
    public GameObject RecipePrefab;
    private List<GameObject> recipes = new List<GameObject>();
    private Recipe selectRecipe;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        Close();
    }


    public void Close()
    {
        canvas.enabled = false;
    }

    public void Refresh()
    {
        DestroyRecipes();
        foreach(Recipe recipe in recipesList.recipes)
        {
            if(Match(recipe))
            {
                CreateRecipe(recipe);
            }
        }
    }

    private void CreateRecipe(Recipe recipe)
    {
        GameObject obj = Instantiate(RecipePrefab);
        obj.transform.SetParent(recipesParent);
        obj.GetComponent<RecipeCell>().SetRecipe(recipe);
        recipes.Add(obj);
    }
    private void DestroyRecipes()
    {
        foreach (GameObject obj in recipes)
            Destroy(obj);
        recipes.Clear();
    }

    public bool Match(Recipe recipe)
    {
        if (recipe.component.Count == component.Length && recipe.fuel.Count == fuel.Length)
        {
            if (recipe.use != MachineUse.Smelter) return false;
            foreach (Piece piece in recipe.component)
            {
                if (!component.ConstainsItem(piece.ID)) return false;
            }
            foreach (Piece piece in recipe.fuel)
            {
                if (!fuel.ConstainsItem(piece.ID)) return false;
            }
            return true;
        }
        return false;
    }
    public void SelectRecipe(Recipe select)
    {
        selectRecipe = select;
    }
    public void Create()
    {
        print(selectRecipe == null);
        if (selectRecipe == null) return;
        NetworkWriter nw = new NetworkWriter(Channels.Reliable);
        nw.SetTypePack(Types.RecipeUse);
        nw.write(selectRecipe.id);
        NetworkManager.Send(nw);
    }
}
