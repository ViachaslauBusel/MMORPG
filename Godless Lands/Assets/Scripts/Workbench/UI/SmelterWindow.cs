using Inventory;
using Items;
using Newtonsoft.Json.Linq;
using Protocol.Data.Items;
using Protocol.Data.Workbenches;
using Recipes;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Workbench;
using Workbench.UI;
using Zenject;

namespace Machines
{
    public class SmelterWindow : MonoBehaviour, IWorkbenchWindow
    {
        [SerializeField]
        private ComponentsDrawer _component;
        [SerializeField]
        private ComponentsDrawer _fuel;
        [SerializeField]
        private Button _createBut;
        [SerializeField]
        private WorkbenchType _workbenchType;
        private Canvas _canvas;
        private SmelterModel _smelterModel;

        private Recipe _selectedRecipe;
        private WorkbenchInputHandler _workbenchInputHandler;

        public Transform recipesParent;
        public GameObject RecipePrefab;
        private List<RecipeCell> recipes = new List<RecipeCell>();
       

      
       
        public MachineUse machineUse;
        private bool _isReadyForWork;


        public WorkbenchType WorkbenchType => _workbenchType;

        [Inject]
        private void Construct(SmelterModel smelterModel)
        {
            _smelterModel = smelterModel;
        }

        private void Awake()
        {
            _workbenchInputHandler = GetComponentInParent<WorkbenchInputHandler>();
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
                else cell.SetCount(_selectedRecipe.component);
            }
            foreach (SmelterCell cell in _fuel.GetCells())
            {
                if (_selectedRecipe == null) cell.DrawCount();
                else cell.SetCount(_selectedRecipe.fuel);
            }

        }
        public void Hide()
        {
            _canvas.enabled = false;
        }
        public void Close()
        {
            _canvas.enabled = false;
            Clear();
            _smelterModel.OnContentUpdate -= OnSmelterModelUpdate;
        }

        public void SetStatus(bool isReadyForWork)
        {
            _isReadyForWork = isReadyForWork;
        }

        public void Open(bool isReadyForWork)
        {
            SetStatus(isReadyForWork);
            _smelterModel.ReserverSmelterModel(_workbenchType);
            Clear();
            _canvas.enabled = true;
            _smelterModel.OnContentUpdate += OnSmelterModelUpdate;
        }

        private void OnSmelterModelUpdate()
        {
            _component.UpdateContent(_smelterModel.Components);
            _fuel.UpdateContent(_smelterModel.Fuels);

            DestroyRecipes();

            foreach (Recipe recipe in _smelterModel.Recipes)
            {
                CreateRecipe(recipe);
            }
            ContainsRecipe(_selectedRecipe);
            RefreshCount();
        }

        private void ContainsRecipe(Recipe recipe)
        {
            if (recipe == null) return;
            if (recipes.Count < 1) SelectRecipe(null);
            foreach (RecipeCell recipeCell in recipes)
            {
                if (recipeCell.GetRecipe().id == recipe.id) { recipeCell.Reselect(); return; }
            }
            SelectRecipe(null);
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

        public void SelectRecipe(Recipe value)
        {
            _selectedRecipe = value;
            _createBut.interactable = _selectedRecipe != null;
            RefreshCount();
        }

        public void Create()
        {
            if (_selectedRecipe == null) return;
            _workbenchInputHandler.CreateItem(_selectedRecipe.id,
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