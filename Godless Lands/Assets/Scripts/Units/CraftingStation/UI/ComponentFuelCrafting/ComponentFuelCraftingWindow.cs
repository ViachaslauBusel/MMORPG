﻿using Items.Data;
using Protocol.Data.Items;
using Protocol.Data.Units.CraftingStation;
using Recipes.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CraftingStations.UI.ComponentFuelCrafting
{
    /// <summary>
    /// Manages windows of a group of stations that can create items from components and fuel
    /// </summary>
    public class ComponentFuelCraftingWindow : MonoBehaviour, ICraftWindow
    {
        [SerializeField]
        private ComponentsDrawer _component;
        [SerializeField]
        private ComponentsDrawer _fuel;
        [SerializeField]
        private Button _createBut;
        [SerializeField]
        private CraftingStationType _stationType;
        private Canvas _canvas;
        private SmelterModel _smelterModel;
        private DiContainer _diContainer;
        private RecipeData _selectedRecipe;
        private CraftingStationController _craftingStationController;

        public Transform recipesParent;
        public GameObject RecipePrefab;
        private List<SmelterRecipeCell> recipes = new List<SmelterRecipeCell>();
        private bool _isReadyForWork;


        public CraftingStationType StationType => _stationType;

        [Inject]
        private void Construct(SmelterModel smelterModel, DiContainer diContainer, CraftingStationController craftingStationController)
        {
            _smelterModel = smelterModel;
            _diContainer = diContainer;
            _craftingStationController = craftingStationController;
        } 

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            _createBut.interactable = false;

            _component.Init(ItemStorageType.SmelerComponent);
            _fuel.Init(ItemStorageType.SmelterFuel);
        }

        private void RefreshCount()
        {
            if (!_canvas.enabled) return;
            foreach (SmelterCell cell in _component.GetCells())
            {
                if (_selectedRecipe == null) cell.DrawCount();
                else cell.SetCount(_selectedRecipe.Components);
            }
            foreach (SmelterCell cell in _fuel.GetCells())
            {
                if (_selectedRecipe == null) cell.DrawCount();
                else cell.SetCount(_selectedRecipe.Fuels);
            }

        }
        public void Hide()
        {
            _canvas.enabled = false;
            _craftingStationController.CloseWindow();
        }
        public void Close()
        {
            _canvas.enabled = false;
            Clear();
            _smelterModel.OnContentUpdate -= OnSmelterModelUpdate;
            _smelterModel.ReleaseSmelterModel();
        }

        public void SetStatus(bool isReadyForWork)
        {
            _isReadyForWork = isReadyForWork;
        }

        public void Open(bool isReadyForWork)
        {
            SetStatus(isReadyForWork);
            _smelterModel.ReserverSmelterModel(_stationType);
            Clear();
            _canvas.enabled = true;
            _smelterModel.OnContentUpdate += OnSmelterModelUpdate;
        }

        private void OnSmelterModelUpdate()
        {
            _component.UpdateContent(_smelterModel.Components);
            _fuel.UpdateContent(_smelterModel.Fuels);

            DestroyRecipes();

            foreach (RecipeData recipe in _smelterModel.Recipes)
            {
                CreateRecipe(recipe);
            }
            ContainsRecipe(_selectedRecipe);
            RefreshCount();
        }

        private void ContainsRecipe(RecipeData recipe)
        {
            if (recipe == null) return;
            if (recipes.Count < 1) SelectRecipe(null);
            foreach (SmelterRecipeCell recipeCell in recipes)
            {
                if (recipeCell.GetRecipe().ID == recipe.ID) { recipeCell.Reselect(); return; }
            }
            SelectRecipe(null);
        }

        private void CreateRecipe(RecipeData recipe)
        {
            GameObject obj = _diContainer.InstantiatePrefab(RecipePrefab);
            obj.transform.SetParent(recipesParent);
            SmelterRecipeCell recipeCell = obj.GetComponent<SmelterRecipeCell>();
            recipeCell.SetRecipe(recipe);
            recipes.Add(recipeCell);
        }
        private void DestroyRecipes()
        {
            foreach (SmelterRecipeCell obj in recipes)
                Destroy(obj.gameObject);
            recipes.Clear();
        }

        public void SelectRecipe(RecipeData value)
        {
            _selectedRecipe = value;
            _createBut.interactable = _selectedRecipe != null;
            RefreshCount();
        }

        public void Create()
        {
            if (_selectedRecipe == null) return;
            _craftingStationController.CreateItem(_selectedRecipe.ID,
                                              _component.GetCells().Where(c => !c.IsEmpty()).Select(c => c.GetItem().UniqueID).ToList(),
                                              _fuel.GetCells().Where(c => !c.IsEmpty()).Select(c => c.GetItem().UniqueID).ToList());
        }

        public void Clear()
        {
            DestroyRecipes();
            _component.Clear();
            _fuel.Clear();
            SelectRecipe(null);
        }
    }
}