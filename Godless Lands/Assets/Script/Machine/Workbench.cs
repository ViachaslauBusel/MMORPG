using System.Collections;
using System.Collections.Generic;
using Cells;
using Items;
using Recipes;
using RUCP;
using UnityEngine;
using UnityEngine.UI;

namespace Machines
{
    public class Workbench : MonoBehaviour, Machine
    {
        // public WorkbenchCell recipeCell;
        public GameObject recipeComponentPref;
        public RecipesList recipesList;
        public Transform content;
        public Text recName;
        public WorkbenchCell cell;
        private Canvas canvas;
        private static Workbench workbench;
        private List<RecipeComponent> components = new List<RecipeComponent>();
        private int machineID;
        private Recipe selectRecipe;

        private void Awake()
        {
            workbench = this;
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }

        private void Start()
        {
            
        //    Refresh(Inventory.GetItem(12));
        }

        public static Recipe GetRecipeByResult(int idItem)
        {
          return workbench.recipesList.GetRecipeByResult(idItem);
        }

        public void Hide()
        {
            canvas.enabled = false;
            Clear();
        }

        public void Open(int id)
        {
            machineID = id;
            canvas.enabled = true;
        }
        public void Open()
        {
        }

        public void PutComponent(int index, Item item, int count)
        {
        //    recipeCell.PutItem(item, count);
        }

        public void PutFuel(int index, Item item, int count)
        {
            throw new System.NotImplementedException();
        }

        public void Create()
        {
            NetworkWriter nw = new NetworkWriter(Channels.Reliable);
            nw.SetTypePack(Types.WorkbenchRecipeUse);
            nw.write(selectRecipe.id);
            nw.write(machineID);
            NetworkManager.Send(nw);
        }

        public void Refresh(Item item)
        {
            RecipesItem recipesItem = (RecipesItem) item.serializableObj;
            if (recipesItem == null) return;
            selectRecipe = recipesList.GetRecipe(recipesItem.recipeID);
            if (selectRecipe == null) return;
            recName.text = item.nameItem;
           // foreach (Piece piece in recipe.component)
          //  {
                GameObject obj = Instantiate(recipeComponentPref);
                obj.transform.SetParent(content);
                RecipeComponent component = obj.GetComponent<RecipeComponent>();
            Piece piece = new Piece();
            piece.ID = selectRecipe.result;
            piece.count = 0;
               
                component.SetPiece(piece);
            component.Start();
            component.Expand();
                components.Add(component);
           // }
        }

        public void Clear()
        {
           foreach(RecipeComponent recipeComponent in components)
            {
                recipeComponent.Destroy();
            }
            components.Clear();
            recName.text = "";
            cell.PutItem(null);
        }


        public void SelectRecipe(Recipe select)
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}