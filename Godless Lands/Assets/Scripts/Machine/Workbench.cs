using System.Collections;
using System.Collections.Generic;
using Cells;
using Items;
using Recipes;
using RUCP;
using RUCP.Network;
using RUCP.Packets;
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
        private RecipeComponent result;
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
            Inventory.UnregisterUpdate(Refresh);
        }

        public void Open(int id)
        {
            machineID = id;
            canvas.enabled = true;
            Inventory.RegisterUpdate(Refresh);
        }
        public void Open()
        {
        }

        public void UpdateComponent(int index, Item item)
        {
        //    recipeCell.PutItem(item, count);
        }

        public void UpdateFuel(int index, Item item)
        {
            throw new System.NotImplementedException();
        }

        public void Create()
        {
            if (selectRecipe == null) return;
            Packet nw = new Packet(Channel.Reliable);
            nw.WriteType(Types.WorkbenchRecipeUse);
            nw.WriteInt(selectRecipe.id);
            nw.WriteInt(machineID);
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
            result = obj.GetComponent<RecipeComponent>();
            Piece piece = new Piece();
            piece.ID = selectRecipe.result;
            piece.count = 0;

            result.SetPiece(piece);
            result.Start();
            result.Expand();
               
           // }
        }

        public void Clear()
        {

            if(result != null)
            result.Destroy();

            result = null;
            recName.text = "Поместите рецепт в ячейку";
            cell.PutItem(null);
        }


        public void SelectRecipe(Recipe select)
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            if (result != null) result.Refresh();
        }
    }
}