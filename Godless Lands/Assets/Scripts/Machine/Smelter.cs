using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recipes;
using RUCP;
using UnityEngine.UI;
using RUCP.Network;
using RUCP.Packets;

namespace Machines
{
    public class Smelter : MonoBehaviour, Machine
    {
        private Canvas canvas;
        public Components component;
        public Components fuel;
        public RecipesList recipesList;
        public Transform recipesParent;
        public GameObject RecipePrefab;
        private List<RecipeCell> recipes = new List<RecipeCell>();
        private Recipe _selectRecipe;
        public Button createBut;
        public MachineUse machineUse;

        private Recipe selectRecipe
        {
            set
            {
                _selectRecipe = value;
                if (_selectRecipe == null)
                    createBut.interactable = false;
                else
                    createBut.interactable = true;
            }
            get { return _selectRecipe; }
        }

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
            createBut.interactable = false;
        }

        public void UpdateComponent(int index, Item item)
        {
            ActionCell actionCell = component.GetCell(index);
            if (actionCell == null) return;
            actionCell.PutItem(item);
            Refresh();
        }

        public void UpdateFuel(int index, Item item)
        {
            ActionCell actionCell = fuel.GetCell(index);
            if (actionCell == null) return;
            actionCell.PutItem(item);
            Refresh();
        }
        private void RefreshCount()
        {
            if (!canvas.enabled) return;
            foreach (ActionCell cell in component.GetCells())
            {
                if (selectRecipe == null) cell.ClearCount();
                else cell.SetCount(selectRecipe.component);
            }
            foreach (ActionCell cell in fuel.GetCells())
            {
                if (selectRecipe == null) cell.ClearCount();
                else cell.SetCount(selectRecipe.fuel);
            }

        }
        public void Close()
        {
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.MachineClose);
            NetworkManager.Send(nw);
        }

        public void Hide()
        {
            canvas.enabled = false;
            Clear();
        }

        public void Open()
        {
            Clear();
            canvas.enabled = true;
        }

        public void Refresh()
        {
            DestroyRecipes();

            foreach (Recipe recipe in recipesList.recipes)
            {
                if (Match(recipe))
                {
                    CreateRecipe(recipe);
                }
            }
            ContainsRecipe(selectRecipe);
            RefreshCount();
        }

        private void ContainsRecipe(Recipe recipe)
        {
            if (recipe == null) return;
            if (recipes.Count < 1) selectRecipe = null;
            foreach(RecipeCell recipeCell in recipes)
            {
                if (recipeCell.GetRecipe().id == recipe.id) { recipeCell.Reselect(); return; }
            }
            selectRecipe = null;
        }

        private void CreateRecipe(Recipe recipe)
        {
            GameObject obj = Instantiate(RecipePrefab);
            obj.transform.SetParent(recipesParent);
            RecipeCell recipeCell = obj.GetComponent<RecipeCell>();
            recipeCell.SetRecipe(recipe);
            recipes.Add(recipeCell);
        }
        private void DestroyRecipes()
        {
            foreach (RecipeCell obj in recipes)
                Destroy(obj.gameObject);
            recipes.Clear();
        }

        public bool Match(Recipe recipe)
        {
            if (recipe.component.Count == component.Length && recipe.fuel.Count == fuel.Length)
            {
                if (recipe.use != machineUse) return false;
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
            RefreshCount();
        }
        public void Create()
        {
            if (selectRecipe == null) return;
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.RecipeUse);
            nw.WriteInt(selectRecipe.id);
            NetworkManager.Send(nw);
        }

        public void Clear()
        {
            DestroyRecipes();
            component.Clear();
            fuel.Clear();
            selectRecipe = null;
        }

    }
}